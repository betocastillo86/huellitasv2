//-----------------------------------------------------------------------
// <copyright file="TextResourceServiceExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions.Services
{
    using System.Collections.Generic;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Services.Configuration;
    using Huellitas.Data.Entities.Enums;

    /// <summary>
    /// Text resources service extensions
    /// </summary>
    public static class TextResourceServiceExtensions
    {
        /// <summary>
        /// Gets all cached settings.
        /// </summary>
        /// <param name="textResourceService">The text resource service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <returns>the list</returns>
        public static IDictionary<string, string> GetAllCachedSettings(this ITextResourceService textResourceService, ICacheManager cacheManager)
        {
            var allKeys = cacheManager.Get(
                CacheKeys.RESOURCES_GET_ALL,
                () =>
                {
                    var dictionarySettings = new Dictionary<string, string>();
                    foreach (var resource in textResourceService.GetAll(LanguageEnum.Spanish))
                    {
                        dictionarySettings.Add(resource.Name, resource.Value);
                    }

                    return dictionarySettings;
                });
            return allKeys;
        }

        /// <summary>
        /// Gets the cached resource.
        /// </summary>
        /// <param name="textResourceService">The text resource service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="key">The key.</param>
        /// <returns>the value</returns>
        public static string GetCachedResource(this ITextResourceService textResourceService, ICacheManager cacheManager, string key)
        {
            string value = string.Empty;
            if (textResourceService.GetAllCachedSettings(cacheManager).TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return key;
            }
        }
    }
}