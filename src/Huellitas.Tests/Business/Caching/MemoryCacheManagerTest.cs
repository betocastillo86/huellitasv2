//-----------------------------------------------------------------------
// <copyright file="MemoryCacheManagerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Caching
{
    using System.Collections.Generic;
    using Huellitas.Business.Caching;
    using Huellitas.Data.Entities;
    using Huellitas.Tests.Business.Mocks;
    using NUnit.Framework;

    /// <summary>
    /// Memory Cache Manager Test
    /// </summary>
    [TestFixture]
    public class MemoryCacheManagerTest
    {
        /// <summary>
        /// Clears the empty memory cache key.
        /// </summary>
        [Test]
        public void ClearEmptyMemoryCacheKey()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            cache.Clear();
            Assert.Zero(memoryCache.Count);
        }

        /// <summary>
        /// Clears the memory cache key.
        /// </summary>
        [Test]
        public void ClearMemoryCacheKey()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            cache.Set("llave1", "valor1", 1);
            cache.Set("llave2", "valor2", 1);
            cache.Clear();

            object value = null;
            Assert.IsFalse(memoryCache.TryGetValue("llave1", out value));
            Assert.Zero(memoryCache.Count);
        }

        /// <summary>
        /// Gets the memory cache key different types.
        /// </summary>
        [Test]
        public void GetMemoryCacheKeyDifferentTypes()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);

            int value1 = 1;
            string key1 = "key1";
            cache.Set(key1, value1, 1);
            Assert.AreEqual(value1, cache.Get<int>(key1));

            Content value2 = new Content() { Name = "Gabriel" };
            string key2 = "key2";
            cache.Set(key2, value2, 1);
            Assert.AreEqual(value2, cache.Get<Content>(key2));

            IList<int> value3 = new List<int>() { 1, 2, 3 };
            string key3 = "key3";
            cache.Set(key3, value3, 1);
            Assert.AreEqual(value3, cache.Get<IList<int>>(key3));
        }

        /// <summary>
        /// Gets the null memory cache key.
        /// </summary>
        [Test]
        public void GetNullMemoryCacheKey()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            Assert.Zero(cache.Get<int>("key1"));
            Assert.IsNull(cache.Get<Content>("key1"));
            Assert.IsNull(cache.Get<string>("key1"));
            Assert.IsNull(cache.Get<IList<int>>("key1"));
            Assert.IsFalse(cache.Get<bool>("key1"));
        }

        /// <summary>
        /// Determines whether [is set cache manager key false].
        /// </summary>
        [Test]
        public void IsSetCacheManagerKey_False()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            Assert.IsFalse(cache.IsSet("llave1"));
        }

        /// <summary>
        /// Determines whether [is set cache manager key true].
        /// </summary>
        [Test]
        public void IsSetCacheManagerKey_True()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            cache.Set("llave1", "valor1", 1);
            Assert.IsTrue(cache.IsSet("llave1"));
        }

        /// <summary>
        /// Removes the by pattern correct.
        /// </summary>
        [Test]
        public void RemoveByPatternCorrect()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            cache.Set("cachekey.content.llave1", "valor1", 1);
            cache.Set("cachekey.content.llave2", "valor1", 1);
            cache.Set("cachekey.content.llave3", "valor1", 1);
            cache.Set("cachekey.user.llave1", "valor1", 1);
            cache.Set("cachekey.user.llave2", "valor1", 1);
            cache.Set("cachekey.user.llave3", "valor1", 1);
            cache.Set("cachekey.user.login.llave1", "valor1", 1);
            cache.Set("cachekey.user.login.llave2", "valor1", 1);
            cache.Set("cachekey.user.login.llave3", "valor1", 1);

            cache.RemoveByPattern("cachekey.content");

            Assert.IsFalse(cache.IsSet("cachekey.content.llave1"));
            Assert.IsFalse(cache.IsSet("cachekey.content.llave2"));
            Assert.IsTrue(cache.IsSet("cachekey.user.llave1"));
            Assert.IsTrue(cache.IsSet("cachekey.user.llave2"));
            Assert.IsTrue(cache.IsSet("cachekey.user.login.llave1"));
            Assert.IsTrue(cache.IsSet("cachekey.user.login.llave2"));

            cache.RemoveByPattern("cachekey.user.login");

            Assert.IsFalse(cache.IsSet("cachekey.content.llave1"));
            Assert.IsFalse(cache.IsSet("cachekey.content.llave2"));
            Assert.IsTrue(cache.IsSet("cachekey.user.llave1"));
            Assert.IsTrue(cache.IsSet("cachekey.user.llave2"));
            Assert.IsFalse(cache.IsSet("cachekey.user.login.llave1"));
            Assert.IsFalse(cache.IsSet("cachekey.user.login.llave2"));
        }

        /// <summary>
        /// Removes the existent cache manager key.
        /// </summary>
        [Test]
        public void RemoveExistentCacheManagerKey()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            cache.Set("llave1", "valor1", 1);
            cache.Remove("llave1");
            Assert.IsFalse(cache.IsSet("llave1"));
        }

        /// <summary>
        /// Removes the not existent cache manager key.
        /// </summary>
        [Test]
        public void RemoveNotExistentCacheManagerKey()
        {
            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);
            cache.Remove("llave1");
            Assert.IsFalse(cache.IsSet("llave1"));
        }

        /// <summary>
        /// Sets the memory cache key.
        /// </summary>
        [Test]
        public void SetMemoryCacheKey()
        {
            string key = "llave1";
            object initialValue = "valor1";
            object value = null;

            var memoryCache = new MemoryCacheMock();
            var cache = new MemoryCacheManager(memoryCache);

            cache.Set(key, initialValue, 1);

            Assert.IsTrue(memoryCache.TryGetValue(key, out value));
            Assert.AreEqual(initialValue, value);
        }
    }
}