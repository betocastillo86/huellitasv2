//-----------------------------------------------------------------------
// <copyright file="CacheEntryMock.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Mocks
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    /// Cache Entry Mock for testing
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Caching.Memory.ICacheEntry" />
    public class CacheEntryMock : ICacheEntry
    {
        /// <summary>
        /// The absolute expiration
        /// </summary>
        private DateTimeOffset? absoluteExpiration;

        /// <summary>
        /// The absolute expiration relative to now
        /// </summary>
        private TimeSpan? absoluteExpirationRelativeToNow;

        /// <summary>
        /// The key
        /// </summary>
        private object key;

        /// <summary>
        /// The priority
        /// </summary>
        private CacheItemPriority priority;

        /// <summary>
        /// The sliding expiration
        /// </summary>
        private TimeSpan? slidingExpiration;

        /// <summary>
        /// The value
        /// </summary>
        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntryMock"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public CacheEntryMock(object key, object value)
        {
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// Gets or sets an absolute expiration date for the cache entry.
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration
        {
            get
            {
                return this.absoluteExpiration;
            }

            set
            {
                this.absoluteExpiration = value;
            }
        }

        /// <summary>
        /// Gets or sets an absolute expiration time, relative to now.
        /// </summary>
        public TimeSpan? AbsoluteExpirationRelativeToNow
        {
            get
            {
                return this.absoluteExpirationRelativeToNow;
            }

            set
            {
                this.absoluteExpirationRelativeToNow = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> instances which cause the cache entry to expire.
        /// </summary>
        public IList<IChangeToken> ExpirationTokens
        {
            get
            {
                return new List<IChangeToken>();
            }
        }

        /// <summary>
        /// Gets the key of the cache entry.
        /// </summary>
        public object Key
        {
            get
            {
                return this.key;
            }
        }

        /// <summary>
        /// Gets the callbacks will be fired after the cache entry is evicted from the cache.
        /// </summary>
        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks
        {
            get
            {
                return new List<PostEvictionCallbackRegistration>();
            }
        }

        /// <summary>
        /// Gets or sets the priority for keeping the cache entry in the cache during a
        /// memory pressure triggered cleanup. The default is <see cref="F:Microsoft.Extensions.Caching.Memory.CacheItemPriority.Normal" />.
        /// </summary>
        public CacheItemPriority Priority
        {
            get
            {
                return this.priority;
            }

            set
            {
                this.priority = value;
            }
        }

        /// <summary>
        /// Gets or sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
        /// This will not extend the entry lifetime beyond the absolute expiration (if set).
        /// </summary>
        public TimeSpan? SlidingExpiration
        {
            get
            {
                return this.slidingExpiration;
            }

            set
            {
                this.slidingExpiration = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the cache entry.
        /// </summary>
        public object Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        public long? Size { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}