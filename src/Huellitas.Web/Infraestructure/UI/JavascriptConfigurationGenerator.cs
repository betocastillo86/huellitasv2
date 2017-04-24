//-----------------------------------------------------------------------
// <copyright file="JavascriptConfigurationGenerator.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;

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
            IRepository<SystemSetting> systemSettingRepository)
        {
            this.generalSettings = generalSettings;
            this.textResourceService = textResourceService;
            this.cacheManager = cacheManager;
            this.env = env;
            this.systemSettingRepository = systemSettingRepository;
        }

        /// <summary>
        /// Creates the <c>javascript</c> configuration file.
        /// </summary>
        public void CreateJavascriptConfigurationFile()
        {
            var isDebug = false;

#if DEBUG
            isDebug = true;
#endif


            this.SaveFile(true, this.GetAdminJson(isDebug));
            this.SaveFile(false, this.GetFrontJson(isDebug));




            ////Actualiza la llave de cache del javascript
            var key = "GeneralSettings.ConfigJavascriptCacheKey";
            var value = this.systemSettingRepository.Table.FirstOrDefault(c => c.Name.Equals(key));
            if (value == null)
            {
                this.systemSettingRepository.Insert(new SystemSetting() { Name = key, Value = Guid.NewGuid().ToString() });
            }
            else
            {
                value.Value = Guid.NewGuid().ToString();
                this.systemSettingRepository.Update(value);
            }

            ////Clears cache after creating file because of the javascript cache key
            this.cacheManager.Clear();
        }

        private string GetAdminJson(bool isDebug)
        {
            var config = new
            {
                general = new
                {
                    pageSize = this.generalSettings.DefaultPageSize
                },
                customTables = new
                {
                    animalSubtype = Convert.ToInt32(CustomTableType.AnimalSubtype),
                    animalSize = Convert.ToInt32(CustomTableType.AnimalSize),
                    animalGenre = Convert.ToInt32(CustomTableType.AnimalGenre)
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
                resources = this.GetAdminResources(),
                isDebug = isDebug
            };

            return JsonConvert.SerializeObject(config);
        }

        private string GetFrontJson(bool isDebug)
        {
            var config = new
            {
                resources = this.GetFrontResources(),
                isDebug = isDebug
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
            return resources;
        }
    }
}