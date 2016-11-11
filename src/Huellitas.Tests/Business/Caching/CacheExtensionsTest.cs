//-----------------------------------------------------------------------
// <copyright file="CacheExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Caching
{
    using Huellitas.Business.Caching;
    using Huellitas.Data.Entities;
    using Huellitas.Tests.Business.Mocks;
    using NUnit.Framework;

    /// <summary>
    /// Cache Extensions Test
    /// </summary>
    [TestFixture]
    public class CacheExtensionsTest
    {
        /// <summary>
        /// Gets the cache value with extension.
        /// </summary>
        [Test]
        public void GetCacheValueWithExtension()
        {
            var cache = this.GetMockCacheManager();

            var valueInt = cache.Get<int>(
                "llave1",
                () =>
                {
                    return 2;
                });
            Assert.AreEqual(2, valueInt);
            Assert.AreEqual(2, cache.Get<int>("llave1"));

            var newContent = new Content() { Name = "Resultado" };
            var valueContent = cache.Get<Content>(
                "llave2", 
                () =>
            {
                return newContent;
            });
            Assert.AreEqual(valueContent, newContent);
            Assert.AreEqual(valueContent, cache.Get<Content>("llave2"));
        }

        /// <summary>
        /// Gets the mock cache manager.
        /// </summary>
        /// <returns>Cache Manager</returns>
        private ICacheManager GetMockCacheManager()
        {
            var memoryCache = new MemoryCacheMock();
            return new MemoryCacheManager(memoryCache);
        }
    }
}