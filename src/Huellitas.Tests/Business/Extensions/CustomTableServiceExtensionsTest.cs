//-----------------------------------------------------------------------
// <copyright file="CustomTableServiceExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Extensions
{
    using System.Collections.Generic;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Test for Custom Table Service Extensions
    /// </summary>
    [TestFixture]
    public class CustomTableServiceExtensionsTest
    {
        /// <summary>
        /// Gets the value by custom table and identifier not null.
        /// </summary>
        [Test]
        public void GetValueByCustomTableAndId_NotNull()
        {
            var mockCustomTableService = this.MockCustomTableService();
            var value = mockCustomTableService.Object.GetValueByCustomTableAndId(CustomTableType.AnimalGenre, 1);
            Assert.AreEqual("a", value);
        }

        /// <summary>
        /// Gets the value by custom table and identifier null.
        /// </summary>
        [Test]
        public void GetValueByCustomTableAndId_Null()
        {
            var mockCustomTableService = this.MockCustomTableService();
            var value = mockCustomTableService.Object.GetValueByCustomTableAndId(CustomTableType.AnimalGenre, 5);
            Assert.IsEmpty(value);
        }

        /// <summary>
        /// Mocks the custom table service.
        /// </summary>
        /// <returns>the mock</returns>
        private Mock<ICustomTableService> MockCustomTableService()
        {
            var mockCustomTableService = new Mock<ICustomTableService>();
            mockCustomTableService.Setup(c => c.GetRowsByTableIdCached(It.IsAny<CustomTableType>())).Returns(new List<CustomTableRow> { new CustomTableRow { Id = 1, Value = "a" }, new CustomTableRow { Id = 2, Value = "b" } });
            return mockCustomTableService;
        }
    }
}