//-----------------------------------------------------------------------
// <copyright file="SeoService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Beto.Core.Data;
    using Business.Configuration;
    using Data.Entities;
    using Data.Entities.Abstract;
    using Huellitas.Business.Extensions;
    using Huellitas.Data.Core;

    /// <summary>
    /// <c>Seo</c> Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ISeoService" />
    public class SeoService : ISeoService
    {
        /// <summary>
        /// The content repository
        /// </summary>
        private readonly IRepository<Content> contentRepository;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeoService"/> class.
        /// </summary>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="contentRepository">The content repository.</param>
        /// <param name="logService">The log service.</param>
        public SeoService(
            IGeneralSettings generalSettings,
            IRepository<Content> contentRepository,
            ILogService logService)
        {
            this.generalSettings = generalSettings;
            this.contentRepository = contentRepository;
            this.logService = logService;
        }

        /// <summary>
        /// Generates the name of the friendly.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="query">The query.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>
        /// the value
        /// </returns>
        public string GenerateFriendlyName(string name, IQueryable<ISeoEntity> query = null, int maxLength = 280)
        {
            ////TODO: Implementar
            ////var wordsToRemove = FactoryContext.Resolve<GeneralSettings>().SeoWordsToRemove.Split(new char[] { ',' });
            ////
            //////Si debe remover articulos los borra
            ////if (removeArticles)
            ////    title = Regex.Replace(title, "\\b" + string.Join("\\b|\\b", wordsToRemove) + "\\b", string.Empty, RegexOptions.IgnoreCase);

            ////Convierte la cadena en Seo friendly
            string textnorm = name.Trim().Normalize(NormalizationForm.FormD);
            var reg = new Regex("[^a-zA-Z0-9 ]");
            string friendlyname = reg.Replace(textnorm, string.Empty)
                .Trim()
                .Replace(" ", "-")
                .ToLower();

            var regexMultipleSpaces = new Regex("[-]{2,}", RegexOptions.None);
            friendlyname = regexMultipleSpaces.Replace(friendlyname, "-");

            if (friendlyname.Length > maxLength)
            {
                ////Valida que el nombre no pase del tamaño permitido, pero agrega las palabras completas en el titulo
                if (friendlyname.IndexOf("-", maxLength - 1) != -1)
                {
                    friendlyname = friendlyname.Substring(0, friendlyname.IndexOf("-", maxLength - 1));
                }
            }

            ////Realiza una ultima validación para verificar que el texto no se vaya ir muy largo.
            ////Ejemplo: cuando meten cadenas muy largas sin espacios
            if (friendlyname.Length > maxLength + 20)
            {
                friendlyname = friendlyname.Substring(0, maxLength + 20);
            }

            if (query != null)
            {
                var available = !query.Any(c => c.FriendlyName.Equals(friendlyname));

                ////Si el nombre no está disponible genera un numero aleatorio para completar la URL
                if (!available)
                {
                    friendlyname = $"{friendlyname}-{new Random().Next(1000000).ToString()}";
                }
            }

            return friendlyname;
        }

        /// <summary>
        /// Generates the site map XML.
        /// </summary>
        /// <returns>the complete site map</returns>
        public string GenerateSiteMapXml()
        {
            var urls = this.GetUrlsForSiteMap();

            var elements = new List<XElement>();
            XNamespace nsa = "http://www.sitemaps.org/schemas/sitemap/0.9";

            foreach (var url in urls)
            {
                var children = new List<XElement>
                {
                    new XElement("loc", url.Key),
                    new XElement("changefreq", "weekly")
                };

                if (url.Value.HasValue)
                {
                    children.Add(new XElement("lastmod", url.Value.Value.ToString("yyyy-MM-dd")));
                }

                var element = new XElement("url", children);
                elements.Add(element);
            }

            var document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement(nsa + "urlset", elements));

            return document.ToString();
        }

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        /// the url
        /// </returns>
        public string GetContentUrl(Content content)
        {
            ////TODO:Test
            switch (content.Type)
            {
                case ContentType.Pet:
                    return this.GetFullRoute("pet", content.FriendlyName);

                case ContentType.Shelter:
                    return this.GetFullRoute("shelter", content.FriendlyName);

                case ContentType.LostPet:
                    return this.GetFullRoute("lostpet", content.FriendlyName);

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the full route of the element
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="parameters">the routes</param>
        /// <returns>
        /// the full route
        /// </returns>
        public string GetFullRoute(string key, params string[] parameters)
        {
            var route = string.Format(this.GetRoute(key), parameters);
            return $"{this.generalSettings.SiteUrl}{(this.generalSettings.SiteUrl.EndsWith("/") ? string.Empty : "/")}{route}";
        }

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="key">The key of the route.</param>
        /// <returns>
        /// the value of the route
        /// </returns>
        public string GetRoute(string key)
        {
            var route = string.Empty;
            if (this.GetRoutes().TryGetValue(key, out route))
            {
                return route;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns>
        /// the routes existent on the web site
        /// </returns>
        public IDictionary<string, string> GetRoutes()
        {
            var routes = new Dictionary<string, string>();
            routes.Add("pets", "sinhogar");
            routes.Add("pet", "sinhogar/{0}");
            routes.Add("adopt0", "sinhogar/{0}/adoptar");
            routes.Add("adopt1", "sinhogar/{0}/adoptar/formulario");
            routes.Add("mypets", "mis-huellitas");
            routes.Add("shelters", "fundaciones");
            routes.Add("shelter", "fundaciones/{0}");
            routes.Add("newpet0", "dar-en-adopcion");
            routes.Add("newpet1", "dar-en-adopcion/crear");
            routes.Add("editpet", "sinhogar/{0}/editar");
            routes.Add("lostpets", "perdidos");
            routes.Add("lostpet", "perdidos/{0}");
            routes.Add("editlostpet", "perdidos/{0}/editar");
            routes.Add("newlostpet", "perdidos/crear");
            routes.Add("myaccount", "mis-datos");
            routes.Add("facebooklogin", "auth/external/facebook");
            routes.Add("home", string.Empty);
            routes.Add("newshelter", "fundaciones/crear");
            routes.Add("editshelter", "fundaciones/{0}/editar");
            routes.Add("forms", "formularios-adopcion");
            routes.Add("form", "formularios-adopcion/{0}");
            routes.Add("notifications", "notificaciones");
            routes.Add("faq", "por-que-adoptar");
            routes.Add("notfound", "pagina-no-encontrada");
            routes.Add("passwordrecovery", "cambiar-clave/{0}");
            return routes;
        }

        /// <summary>
        /// Gets the content url.
        /// </summary>
        /// <returns>the list of url</returns>
        private IDictionary<string, DateTime?> GetContentUrls()
        {
            var statusPublished = Convert.ToInt16(StatusType.Published);
            //var statusRejected = Convert.ToInt16(StatusType.Rejected);
            //var statusClosed = Convert.ToInt16(StatusType.Closed);
            var contents = this.contentRepository.Table
                .Where(c => !c.Deleted && c.Status == statusPublished && (c.ClosingDate == null || c.ClosingDate >= DateTime.Now))
                .ToList();

            var urls = new Dictionary<string, DateTime?>();

            foreach (var content in contents)
            {
                if (!string.IsNullOrEmpty(content.FriendlyName))
                {
                    try
                    {
                        urls.Add(this.GetContentUrl(content), content.UpdatedDate ?? content.CreatedDate);
                    }
                    catch (Exception e)
                    {
                        this.logService.Error(e);
                    }
                }
            }

            return urls;
        }

        /// <summary>
        /// Gets the url for site map.
        /// </summary>
        /// <returns>the url</returns>
        private IDictionary<string, DateTime?> GetUrlsForSiteMap()
        {
            var urls = new Dictionary<string, DateTime?>();
            urls.Add($"{this.generalSettings.SiteUrl}", null);
            urls.Add(this.GetFullRoute("shelters"), null);
            urls.Add(this.GetFullRoute("pets"), null);
            urls.Add(this.GetFullRoute("lostpets"), null);
            urls.Add(this.GetFullRoute("newshelter"), null);
            urls.Add(this.GetFullRoute("newlostpet"), null);
            urls.Add(this.GetFullRoute("faq"), null);
            urls.Add(this.GetFullRoute("newpet0"), null);

            foreach (var content in this.GetContentUrls())
            {
                urls.Add(content.Key, content.Value);
            }

            return urls;
        }
    }
}