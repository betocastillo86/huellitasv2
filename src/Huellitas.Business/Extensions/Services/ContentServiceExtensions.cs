//-----------------------------------------------------------------------
// <copyright file="ContentServiceExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Business.Services.Contents;
    using Caching;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Content Service Extensions
    /// </summary>
    public static class ContentServiceExtensions
    {
        /// <summary>
        /// Gets the cached shelter.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>the cached shelter</returns>
        public static Content GetCachedShelter(this IContentService service, ICacheManager cacheManager, int id)
        {
            ////TODO:Test
            return GetCachedShelters(service, cacheManager).FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Gets the cached shelters.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <returns>all the shelters</returns>
        private static IList<Content> GetCachedShelters(this IContentService service, ICacheManager cacheManager)
        {
            return cacheManager.Get(
                CacheKeys.SHELTERS_ALL,
                () =>
                {
                    return service.Search(contentType: ContentType.Shelter);
                });
        }
    }
}