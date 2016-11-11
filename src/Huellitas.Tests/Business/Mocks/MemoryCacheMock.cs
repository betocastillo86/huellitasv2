//-----------------------------------------------------------------------
// <copyright file="MemoryCacheMock.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// Memory cache mock for testing
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Caching.Memory.IMemoryCache" />
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:FieldNamesMustNotBeginWithUnderscore", Justification = "Reviewed.")]
    public class MemoryCacheMock : IMemoryCache
    {
        /// <summary>
        /// The entries
        /// </summary>
        private IDictionary<object, ICacheEntry> _entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheMock"/> class.
        /// </summary>
        public MemoryCacheMock()
        {
            this._entries = new Dictionary<object, ICacheEntry>();
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get
            {
                return this._entries.Count;
            }
        }

        /// <summary>
        /// Create or overwrite an entry in the cache.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        /// <returns>
        /// The newly created <see cref="T:Microsoft.Extensions.Caching.Memory.ICacheEntry" /> instance.
        /// </returns>
        public ICacheEntry CreateEntry(object key)
        {
            var entry = new CacheEntryMock(key, null);
            this._entries.Add(key, entry);
            return entry;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ////No implementado
        }

        /// <summary>
        /// Removes the object associated with the given key.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        public void Remove(object key)
        {
            this._entries.Remove(key);
        }

        /// <summary>
        /// Gets the item associated with this key if present.
        /// </summary>
        /// <param name="key">An object identifying the requested entry.</param>
        /// <param name="value">The located value or null.</param>
        /// <returns>
        /// True if the key was found.
        /// </returns>
        public bool TryGetValue(object key, out object value)
        {
            ICacheEntry cacheEntry = null;
            var exists = this._entries.TryGetValue(key, out cacheEntry);
            value = exists ? cacheEntry.Value : null;
            return exists;
        }
    }
}