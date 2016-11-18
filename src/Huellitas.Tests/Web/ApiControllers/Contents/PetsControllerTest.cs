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
    using Huellitas.Business.Caching;
    using Huellitas.Business.Services.Common;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Files;
    using Huellitas.Web.Controllers.Api.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;
    using Huellitas.Web.Infraestructure.Security;

    /// <summary>
    /// Pets Controller Test
    /// </summary>
    [TestFixture]
    public class PetsControllerTest : BaseTest
    {
        /// <summary>
        /// Gets the pets invalid filter.
        /// </summary>
        [Test]
        public void GetPetsInvalidFilter()
        {
            var mockContentService = new Mock<IContentService>();
            var fileHelpers = new Mock<IFilesHelper>();
            var customTableService = new Mock<ICustomTableService>();
            var cacheManager = new Mock<ICacheManager>();

            var controller = new PetsController(mockContentService.Object, fileHelpers.Object, cacheManager.Object, customTableService.Object, this.WorkContextMock.Object);

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
            var fileHelpers = new Mock<IFilesHelper>();
            var customTableService = new Mock<ICustomTableService>();
            var cacheManager = new Mock<ICacheManager>();

            mockContentService.Setup(c => c.Search(null, ContentType.Pet, new List<FilterAttribute>(), 10, 0, ContentOrderBy.DisplayOrder)).Returns(new PagedList<Content>() { new Content() { } });

            var controller = new PetsController(mockContentService.Object, fileHelpers.Object, cacheManager.Object, customTableService.Object, this.WorkContextMock.Object);
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

        /// <summary>
        /// Posts the pets bad request.
        /// </summary>
        [Test]
        public void PostPetsBadRequest()
        {
            var mockContentService = new Mock<IContentService>();
            var fileHelpers = new Mock<IFilesHelper>();
            var customTableService = new Mock<ICustomTableService>();
            var cacheManager = new Mock<ICacheManager>();

            var controller = new PetsController(mockContentService.Object, fileHelpers.Object, cacheManager.Object, customTableService.Object, this.WorkContextMock.Object);

            var model = new PetModel();
            var response = controller.Post(model) as ObjectResult;

            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("Images", error.Details[0].Target);
            Assert.AreEqual("Shelter", error.Details[1].Target);
            Assert.AreEqual("Location", error.Details[2].Target);
        }

        /// <summary>
        /// Posts the pets <c>ok</c>.
        /// </summary>
        [Test]
        public void PostPetsOk()
        {
            var mockContentService = new Mock<IContentService>();
            var fileHelpers = new Mock<IFilesHelper>();
            var customTableService = new Mock<ICustomTableService>();
            var cacheManager = new Mock<ICacheManager>();
            var model = new PetModel().MockNew();

            int newId = 1;

            var content = model.ToEntity(mockContentService.Object);
            mockContentService.Setup(c => c.Insert(It.IsAny<Content>()))
                .Callback((Content content1) =>
                {
                    content1.Id = newId;
                });

            var controller = new PetsController(mockContentService.Object, fileHelpers.Object, cacheManager.Object, customTableService.Object, this.WorkContextMock.Object);
            controller.AddUrl(true);

            var response = controller.Post(model) as ObjectResult;

            Assert.AreEqual(201, response.StatusCode);
            Assert.IsTrue(controller.IsValidModelState(model));
            Assert.AreEqual(newId, (response.Value as BaseModel).Id);
        }
    }
}