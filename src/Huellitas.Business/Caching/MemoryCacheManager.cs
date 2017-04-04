//-----------------------------------------------------------------------
// <copyright file="MemoryCacheManager.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// The Cache Manager
    /// </summary>
    /// <seealso cref="Huellitas.Business.Caching.ICacheManager" />
    public class MemoryCacheManager : ICacheManager
    {
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
            var keys = this.GetAllKeys().Select(c => c.Key).ToList();
            foreach (var key in keys)
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
        }

        /// <summary>
        /// Removes the by pattern.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keys = this.GetAllKeys();

            var keysToRemove = new List<string>();

            foreach (var cacheEntry in keys)
            {
                var key = cacheEntry.Value.Key.ToString();
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
        }

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <returns>the existent keys</returns>
        private IDictionary<object, ICacheEntry> GetAllKeys()
        {
            ////Due to the keys of Memory Cache are private it's necessary to access through reflection
            ////http://stackoverflow.com/questions/37452962/get-all-cache-in-asp-net-core-1
            return (Dictionary<object, ICacheEntry>)this.memoryCache.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(c => c.Name.Equals("_entries")).GetValue(this.memoryCache);
        }
    }
}