//-----------------------------------------------------------------------
// <copyright file="PetsControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System.Collections.Generic;
    using Data.Entities;
    using Data.Infraestructure;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Web.Controllers.Api.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Contents;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Pets Controller Test
    /// </summary>
    [TestFixture]
    public class PetsControllerTest
    {
        /// <summary>
        /// Gets the pets invalid filter.
        /// </summary>
        [Test]
        public void GetPetsInvalidFilter()
        {
            var mockContentService = new Mock<IContentService>();

            var controller = new PetsController(mockContentService.Object);

            var filter = new PetsFilterModel();
            filter.Shelter = "4,b,c,5";

            var response = controller.Get(filter) as ObjectResult;
            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("BadArgument", error.Code);
            Assert.AreEqual("Shelter", error.Details[0].Target);
        }

        /// <summary>
        /// Gets the pets valid filter.
        /// </summary>
        [Test]
        public void GetPetsValidFilter()
        {
            var mockContentService = new Mock<IContentService>();
            mockContentService.Setup(c => c.Search(null, ContentType.Pet, new List<FilterAttribute>(), 10, 0, ContentOrderBy.DisplayOrder)).Returns(new PagedList<Content>() { new Content() { } });

            var controller = new PetsController(mockContentService.Object);
            controller.AddResponse().AddUrl();

            var filter = new PetsFilterModel();
            var reponse = controller.Get(filter) as ObjectResult;
            var list = reponse.Value as IList<PetModel>;

            Assert.AreEqual(200, reponse.StatusCode);
            Assert.AreEqual("False", controller.Response.Headers[ApiHeadersList.PAGINATION_HASNEXTPAGE].ToString());
            Assert.AreEqual("1", controller.Response.Headers[ApiHeadersList.PAGINATION_COUNT].ToString());
            Assert.AreEqual("0", controller.Response.Headers[ApiHeadersList.PAGINATION_TOTALCOUNT].ToString());
            Assert.AreEqual(1, list.Count);
        }
    }
}