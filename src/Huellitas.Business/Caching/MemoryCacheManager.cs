//-----------------------------------------------------------------------
// <copyright file="MemoryCacheManager.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// The Cache Manager
    /// </summary>
    /// <seealso cref="Huellitas.Business.Caching.ICacheManager" />
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// The current keys
        /// </summary>
        private static HashSet<string> currentKeys = new HashSet<string>();

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheManager"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Clears all the cache
        /// </summary>
        public void Clear()
        {
            foreach (var key in currentKeys)
            {
                this.memoryCache.Remove(key);
            }
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T">any type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// the value in cache
        /// </returns>
        public T Get<T>(string key)
        {
            object value = null;
            if (this.memoryCache.TryGetValue(key, out value))
            {
                return (T)value;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Determines whether the specified key is set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key is set; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSet(string key)
        {
            object value = null;
            return this.memoryCache.TryGetValue(key, out value);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            this.memoryCache.Remove(key);
            currentKeys.Remove(key);
        }

        /// <summary>
        /// Removes the by pattern.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var keysToRemove = new List<string>();

            foreach (var key in currentKeys)
            {
                if (regex.IsMatch(key))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var keyToRemove in keysToRemove)
            {
                this.Remove(keyToRemove);
            }
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="cacheTime">The cache time in minutes.</param>
        public void Set(string key, object data, int cacheTime)
        {
            this.memoryCache.Set(key, data, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTime)));
            currentKeys.Add(key);
        }
    }
}