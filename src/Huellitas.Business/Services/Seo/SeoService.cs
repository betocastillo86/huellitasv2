//-----------------------------------------------------------------------
// <copyright file="SeoService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Business.Configuration;
    using Data.Entities;
    using Data.Entities.Abstract;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// <c>Seo</c> Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ISeoService" />
    public class SeoService : ISeoService
    {
        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeoService"/> class.
        /// </summary>
        /// <param name="generalSettings">The general settings.</param>
        public SeoService(IGeneralSettings generalSettings)
        {
            this.generalSettings = generalSettings;
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
                var isAvailable = !query.Any(c => c.FriendlyName.Equals(friendlyname));

                ////Si el nombre no está disponible genera un numero aleatorio para completar la URL
                if (!isAvailable)
                {
                    friendlyname = $"{friendlyname}-{new Random().Next(1000000).ToString()}";
                }
            }

            return friendlyname;
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
        /// <param name="key"></param>
        /// <param name="parameters"></param>
        /// <returns>the full route</returns>
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
            routes.Add("mypets", "mis-huellitas");
            routes.Add("pet", "sinhogar/{0}");
            routes.Add("lostpet", "perdidos/{0}");
            routes.Add("shelter", "fundaciones/{0}");
            return routes;
        }
    }
}