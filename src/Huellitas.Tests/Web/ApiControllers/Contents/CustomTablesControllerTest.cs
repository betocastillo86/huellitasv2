//-----------------------------------------------------------------------
// <copyright file="CustomTablesControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System.Collections.Generic;
    using Huellitas.Business.Services.Common;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Controllers.Api.Common;
    using Huellitas.Web.Models.Api.Common;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Custom Tables Controller Test
    /// </summary>
    [TestFixture]
    public class CustomTablesControllerTest
    {
        /// <summary>
        /// Gets the table rows by table ok.
        /// </summary>
        [Test]
        public void GetTableRowsByTable_Ok()
        {
            var mockCustomTableService = new Mock<ICustomTableService>();
            mockCustomTableService.Setup(c => c.GetRowsByTableId(It.IsAny<int>()))
                .Returns(new List<CustomTableRow> { new CustomTableRow { Id = 1 }, new CustomTableRow { Id = 2 } });

            var controller = new CustomTablesController(mockCustomTableService.Object);

            var reponse = controller.GetByTable(1) as ObjectResult;
            var list = reponse.Value as List<CustomTableRowModel>;

            Assert.AreEqual(200, reponse.StatusCode);
            Assert.AreEqual(2, list.Count);
        }
    }
}