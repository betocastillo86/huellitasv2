//-----------------------------------------------------------------------
// <copyright file="JavascriptConfigurationGenerator.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.UI
{
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The <c>javascript</c> Configuration Generator
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.UI.IJavascriptConfigurationGenerator" />
    public class JavascriptConfigurationGenerator : IJavascriptConfigurationGenerator
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The environment
        /// </summary>
        private readonly IHostingEnvironment env;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The system setting repository
        /// </summary>
        private readonly IRepository<SystemSetting> systemSettingRepository;

        /// <summary>
        /// The text resource service
        /// </summary>
        private readonly ITextResourceService textResourceService;

        /// <summary>
        /// Custom table service
        /// </summary>
        private readonly ICustomTableService customTableService;

        /// <summary>
        /// The seo service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The security
        /// </summary>
        private readonly ISecuritySettings securitySettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptConfigurationGenerator"/> class.
        /// </summary>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="textResourceService">The text resource service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="env">The env.</param>
        /// <param name="systemSettingRepository">The system setting repository.</param>
        public JavascriptConfigurationGenerator(
            IGeneralSettings generalSettings,
            ITextResourceService textResourceService,
            ICacheManager cacheManager,
            IHostingEnvironment env,
            IRepository<SystemSetting> systemSettingRepository,
            ICustomTableService customTableService,
            ISeoService seoService,
            ISecuritySettings securitySettings)
        {
            this.generalSettings = generalSettings;
            this.textResourceService = textResourceService;
            this.cacheManager = cacheManager;
            this.env = env;
            this.systemSettingRepository = systemSettingRepository;
            this.customTableService = customTableService;
            this.seoService = seoService;
            this.securitySettings = securitySettings;
        }

        /// <summary>
        /// Creates the <c>javascript</c> configuration file.
        /// </summary>
        public void CreateJavascriptConfigurationFile()
        {
            this.cacheManager.Clear();

            var isDebug = false;

#if DEBUG
            isDebug = true;
#endif

            ////Actualiza la llave de cache del javascript
            var key = "GeneralSettings.ConfigJavascriptCacheKey";
            var cacheKey = this.systemSettingRepository.Table.FirstOrDefault(c => c.Name.Equals(key));


            this.SaveFile(true, this.GetAdminJson(isDebug, cacheKey?.Value));
            this.SaveFile(false, this.GetFrontJson(isDebug, cacheKey?.Value));

            
            if (cacheKey == null)
            {
                this.systemSettingRepository.Insert(new SystemSetting() { Name = key, Value = Guid.NewGuid().ToString() });
            }
            else
            {
                cacheKey.Value = Guid.NewGuid().ToString();
                this.systemSettingRepository.Update(cacheKey);
            }

            ////Clears cache after creating file because of the javascript cache key
            this.cacheManager.Clear();
        }

        private string GetAdminJson(bool isDebug, string cacheKey)
        {
            var config = new
            {
                general = new
                {
                    pageSize = this.generalSettings.DefaultPageSize,
                    configJavascriptCacheKey = cacheKey,
                    siteUrl = generalSettings.SiteUrl
                },
                customTables = new
                {
                    animalSubtype = Convert.ToInt32(CustomTableType.AnimalSubtype),
                    animalSize = Convert.ToInt32(CustomTableType.AnimalSize),
                    animalGenre = Convert.ToInt32(CustomTableType.AnimalGenre),
                    breed = Convert.ToInt32(CustomTableType.Breed)
                },
                statusTypes = new
                {
                    created = Convert.ToInt32(StatusType.Created),
                    published = Convert.ToInt32(StatusType.Published),
                    hidden = Convert.ToInt32(StatusType.Hidden),
                    closed = Convert.ToInt32(StatusType.Closed)
                },
                contentRelationTypes = new
                {
                    parent = ContentUserRelationType.Parent.ToString(),
                    shelter = ContentUserRelationType.Shelter.ToString()
                },
                security = new
                {
                    maxRequestFileUploadMB = securitySettings.MaxRequestFileUploadMB
                },
                resources = this.GetAdminResources(),
                isDebug = isDebug,
                isFront = false,
                routes = this.seoService.GetRoutes()
            };

            return JsonConvert.SerializeObject(config);
        }

        private string GetFrontJson(bool isDebug, string cacheKey)
        {
            var config = new
            {
                general = new
                {
                    facebookPublicToken = generalSettings.FacebookPublicToken,
                    siteUrl = generalSettings.SiteUrl,
                    seoImage = generalSettings.SeoImage,
                    googleAnalyticsCode = generalSettings.GoogleAnalyticsCode,
                    configJavascriptCacheKey = cacheKey,
                    adsenseEnabled = generalSettings.AdsenseEnabled
                },
                resources = this.GetFrontResources(),
                isDebug = isDebug,
                subtypes = this.customTableService.GetRowsByTableIdCached(CustomTableType.AnimalSubtype).Select(c => new { id = c.Id, value = c.Value }),
                sizes = this.customTableService.GetRowsByTableIdCached(CustomTableType.AnimalSize).Select(c => new { id = c.Id, value = c.Value }),
                genres = this.customTableService.GetRowsByTableIdCached(CustomTableType.AnimalGenre).Select(c => new { id = c.Id, value = c.Value }),
                customTables = new
                {
                    questionAdoptionForm = Convert.ToInt32(CustomTableType.QuestionAdoptionForm),
                    jobs = Convert.ToInt32(CustomTableType.Jobs),
                    breed = Convert.ToInt32(CustomTableType.Breed)
                },
                security = new
                {
                    maxRequestFileUploadMB = securitySettings.MaxRequestFileUploadMB
                },
                isFront = true,
                routes = this.seoService.GetRoutes()
            };

            return JsonConvert.SerializeObject(config);
        }

        private void SaveFile(bool isAdmin, string jsonString)
        {
            ////If does not exist the directory. It creates it.
            var filename = $"{env.ContentRootPath}/wwwroot/js/{(isAdmin ? "admin" : "front")}.configuration.js";
            var directory = System.IO.Path.GetDirectoryName(filename);

            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(filename, FileMode.Create))
            {
                using (var sw = new System.IO.StreamWriter(stream))
                {
                    sw.Write($"var app = app || {{}}; app.Settings = {jsonString.ToString()}");
                }
            }
        }

        /// <summary>
        /// Adds the resources.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <param name="key">The key.</param>
        private void AddResources(IDictionary<string, string> resources, string key)
        {
            resources.Add(key, this.textResourceService.GetCachedResource(this.cacheManager, key));
        }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <returns>the resources</returns>
        private IDictionary<string, string> GetAdminResources()
        {
            var resources = new Dictionary<string, string>();
            this.AddResources(resources, "UserRole.Public");
            this.AddResources(resources, "UserRole.SuperAdmin");
            return resources;
        }

        private IDictionary<string, string> GetFrontResources()
        {
            var resources = new Dictionary<string, string>();
            this.AddResources(resources, "Home.HowTo.Help.Title");
            this.AddResources(resources, "Home.HowTo.Help.Content");
            this.AddResources(resources, "Home.HowTo.Parent.Title");
            this.AddResources(resources, "Home.HowTo.Parent.Content");
            this.AddResources(resources, "Home.HowTo.Adopt.Title");
            this.AddResources(resources, "Home.HowTo.Adopt.Content");
            this.AddResources(resources, "Home.Who.Title");
            this.AddResources(resources, "Home.Who.Content");
            this.AddResources(resources, "Seo.Home.Title");
            this.AddResources(resources, "Seo.Home.Description");
            this.AddResources(resources, "Seo.EditPet.Title");
            this.AddResources(resources, "Seo.EditPet.Description");
            this.AddResources(resources, "Seo.Adopt.Title");
            this.AddResources(resources, "Seo.Adopt.Description");
            this.AddResources(resources, "Seo.AdoptTerms.Title");
            this.AddResources(resources, "Seo.AdoptTerms.Description");
            this.AddResources(resources, "Seo.PetDetail.Title");
            this.AddResources(resources, "Seo.Pets.Title");
            this.AddResources(resources, "Seo.Pets.Description");
            this.AddResources(resources, "Seo.EditShelter.Title");
            this.AddResources(resources, "Seo.EditShelter.Description");
            this.AddResources(resources, "Seo.ShelterDetail.Title");
            this.AddResources(resources, "Seo.LostPets.Title");
            this.AddResources(resources, "Seo.LostPets.Description");
            this.AddResources(resources, "Seo.EditLostPet.Title");
            this.AddResources(resources, "Seo.EditLostPet.Description");
            this.AddResources(resources, "Seo.LostPetDetail.Title");
            this.AddResources(resources, "Seo.Shelters.Title");
            this.AddResources(resources, "Seo.Shelters.Description");
            this.AddResources(resources, "Seo.NewPet.Title");
            this.AddResources(resources, "Seo.NewPet.Description");
            this.AddResources(resources, "Seo.AdoptionFormDetail.Title");
            this.AddResources(resources, "Seo.AdoptionFormDetail.Description");
            this.AddResources(resources, "Seo.AdoptionForms.Title");
            this.AddResources(resources, "Seo.AdoptionForms.Description");
            this.AddResources(resources, "Seo.MyPets.Title");
            this.AddResources(resources, "Seo.MyPets.Description");
            this.AddResources(resources, "Seo.MyNotifications.Title");
            this.AddResources(resources, "Seo.MyNotifications.Description");
            this.AddResources(resources, "Seo.Faq.Title");
            this.AddResources(resources, "Seo.Faq.Description");

            return resources;
        }
    }
}