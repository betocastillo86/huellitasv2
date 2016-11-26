//-----------------------------------------------------------------------
// <copyright file="ISeoService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Seo
{
    using System.Linq;
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
    }
}