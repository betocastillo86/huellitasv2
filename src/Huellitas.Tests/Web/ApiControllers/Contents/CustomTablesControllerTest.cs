//-----------------------------------------------------------------------
// <copyright file="CustomTablesControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System.Collections.Generic;
    using System.Linq;
    using Beto.Core.Data;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Models.Api;
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
            mockCustomTableService.Setup(c => c.GetRowsByTableId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<OrderByTableRow>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<CustomTableRow>(new List<CustomTableRow> { new CustomTableRow { Id = 1 }, new CustomTableRow { Id = 2 } }.AsQueryable(), 0, int.MaxValue));

            var controller = new CustomTablesController(mockCustomTableService.Object);

            var reponse = controller.GetByTable(1, new CustomTableRowFilter { }) as ObjectResult;
            var list = reponse.Value as List<CustomTableRowModel>;

            Assert.AreEqual(200, reponse.StatusCode);
        }
    }
}