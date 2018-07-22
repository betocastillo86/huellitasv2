//-----------------------------------------------------------------------
// <copyright file="ISeoService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Beto.Core.Data.Common;
    using Data.Entities;

    /// <summary>
    /// Interface to friendly
    /// </summary>
    public interface ISeoService
    {
        /// <summary>
        /// Generates the name of the friendly.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="query">The query of SEO entities to validate if the friendly name already exists</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>the value</returns>
        string GenerateFriendlyName(string name, IQueryable<ISeoEntity> query = null, int maxLength = 280);

        /// <summary>
        /// Generates the site map XML.
        /// </summary>
        /// <returns>the site map</returns>
        string GenerateSiteMapXml();

        /// <summary>
        /// Gets the content URL.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the url</returns>
        string GetContentUrl(Content content);

        /// <summary>
        /// Gets the full route of the element
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="parameters">the routes</param>
        /// <returns>the full route</returns>
        string GetFullRoute(string key, params string[] parameters);

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="key">The key of the route.</param>
        /// <returns>the value of the route</returns>
        string GetRoute(string key);

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns>the routes existent on the web site</returns>
        IDictionary<string, string> GetRoutes();
    }
}