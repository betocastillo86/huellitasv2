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
        /// Gets the shelter by pet.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="petId">The pet identifier.</param>
        /// <returns>the shelter</returns>
        public static Content GetShelterByPet(this IContentService service, int petId)
        {
            ////TODO:Test
            var shelterId = service.GetContentAttribute<int?>(petId, ContentAttributeType.Shelter);

            if (shelterId.HasValue)
            {
                return service.GetById(shelterId.Value);
            }

            return null;
        }

        /// <summary>
        /// Gets the cached shelter.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>the cached shelter</returns>
        public static Content GetCachedShelter(this IContentService service, ICacheManager cacheManager, int id)
        {
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