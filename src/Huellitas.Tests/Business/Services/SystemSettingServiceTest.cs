//-----------------------------------------------------------------------
// <copyright file="SystemSettingServiceTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Services;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// System Setting Service Test
    /// </summary>
    [TestFixture]
    public class SystemSettingServiceTest : BaseTest
    {
        /// <summary>
        /// Gets the cached setting typed.
        /// </summary>
        [Test]
        public void GetCachedSettingTyped()
        {
            var service = this.GetMockService();
            Assert.AreEqual(1, service.GetCachedSetting<int>("key1"));
            Assert.AreEqual("a", service.GetCachedSetting<string>("key2"));
            Assert.IsTrue(service.GetCachedSetting<bool>("key3"));
        }

        /// <summary>
        /// Gets the mock service.
        /// </summary>
        /// <returns>the service</returns>
        private SystemSettingService GetMockService()
        {
            var cacheManagerMock = new Mock<ICacheManager>();

            var listKeys = new List<SystemSetting>();
            listKeys.Add(new SystemSetting() { Name = "key1", Value = "1" });
            listKeys.Add(new SystemSetting() { Name = "key2", Value = "a" });
            listKeys.Add(new SystemSetting() { Name = "key3", Value = "True" });

            var systemRepositoryMock = new Mock<IRepository<SystemSetting>>();
            systemRepositoryMock.Setup(c => c.TableNoTracking)
                .Returns(listKeys.AsQueryable());

            return new SystemSettingService(systemRepositoryMock.Object, cacheManagerMock.Object, this.publisher.Object);
        }
    }
}