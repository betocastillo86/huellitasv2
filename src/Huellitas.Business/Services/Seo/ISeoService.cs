//-----------------------------------------------------------------------
// <copyright file="ISeoService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Linq;
    using Data.Entities;
    using Huellitas.Data.Entities.Abstract;

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
        /// Gets the content URL.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the url</returns>
        string GetContentUrl(Content content);
    }
}