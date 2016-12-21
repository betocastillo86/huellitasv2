//-----------------------------------------------------------------------
// <copyright file="PetsControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Infraestructure;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services.Common;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Files;
    using Huellitas.Web.Controllers.Api.Common;
    using Huellitas.Web.Controllers.Api.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Api.Files;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;

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

            var controller = this.MockController();

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
            var pictureService = new Mock<IPictureService>();
            var contentSettings = new Mock<IContentSettings>();
            var fileService = new Mock<IFileService>();

            mockContentService.Setup(c => c.Search(null, ContentType.Pet, new List<FilterAttribute>(), 10, 0, ContentOrderBy.DisplayOrder)).Returns(new PagedList<Content>() { new Content() { } });

            var controller = new PetsController(
                mockContentService.Object, 
                fileHelpers.Object, 
                cacheManager.Object, 
                customTableService.Object, 
                this.WorkContextMock.Object,
                pictureService.Object,
                contentSettings.Object,
                fileService.Object);

            controller.AddResponse().AddUrl();

            var filter = new PetsFilterModel();
            var reponse = controller.Get(filter) as ObjectResult;
            var list = reponse.Value as PaginationResponseModel<PetModel>;

            Assert.AreEqual(200, reponse.StatusCode);
            Assert.IsFalse(list.Meta.HasNextPage);
            Assert.AreEqual(1, list.Meta.Count);
            Assert.AreEqual(0, list.Meta.TotalCount);
            Assert.AreEqual(1, list.Results.Count);
        }

        /// <summary>
        /// Posts the pets bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostPetsBadRequest()
        {
            var mockContentService = new Mock<IContentService>();
            var fileHelpers = new Mock<IFilesHelper>();
            var customTableService = new Mock<ICustomTableService>();
            var cacheManager = new Mock<ICacheManager>();
            var pictureService = new Mock<IPictureService>();
            var contentSettings = new Mock<IContentSettings>();
            var fileService = new Mock<IFileService>();

            var controller = new PetsController(
                mockContentService.Object, 
                fileHelpers.Object, 
                cacheManager.Object, 
                customTableService.Object, 
                this.WorkContextMock.Object, 
                pictureService.Object, 
                contentSettings.Object,
                fileService.Object);

            var model = new PetModel();
            var response = await controller.Post(model) as ObjectResult;

            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("Files", error.Details[0].Target);
            Assert.AreEqual("Shelter", error.Details[1].Target);
            Assert.AreEqual("Location", error.Details[2].Target);
        }

        /// <summary>
        /// Posts the pets ok.
        /// </summary>
        /// <returns>The task</returns>
        [Test]
        public async Task PostPetsOk()
        {
            var mockContentService = new Mock<IContentService>();
            var fileHelpers = new Mock<IFilesHelper>();
            var customTableService = new Mock<ICustomTableService>();
            var cacheManager = new Mock<ICacheManager>();
            var pictureService = new Mock<IPictureService>();
            var contentSettings = new Mock<IContentSettings>();

            var fileService = new Mock<IFileService>();
            fileService.Setup(c => c.GetByIds(It.IsAny<int[]>()))
                .Returns(new List<File>());

            var model = new PetModel().MockNew();

            int newId = 1;

            var content = model.ToEntity(mockContentService.Object);
            mockContentService.Setup(c => c.InsertAsync(It.IsAny<Content>()))
                .Callback((Content content1) =>
                {
                    content1.Id = newId;
                })
                .Returns(Task.FromResult(0));

            var controller = new PetsController(
                mockContentService.Object, 
                fileHelpers.Object, 
                cacheManager.Object, 
                customTableService.Object, 
                this.WorkContextMock.Object,
                pictureService.Object,
                contentSettings.Object,
                fileService.Object);

            controller.AddUrl(true);

            var response = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(201, response.StatusCode);
            Assert.IsTrue(controller.IsValidModelState(model));
            Assert.AreEqual(newId, (response.Value as BaseModel).Id);
        }

        /// <summary>
        /// Determines whether [is valid model true].
        /// </summary>
        [Test]
        public void IsValidModel_New_True()
        {
            var controller = this.MockController();
            var model = new PetModel();
            model.Files = new List<FileModel>() { new FileModel() };
            model.Shelter = new ShelterModel();
            Assert.IsTrue(controller.IsValidModel(model, true));

            model.Shelter = null;
            model.Location = new Huellitas.Web.Models.Api.Common.LocationModel();
            Assert.IsTrue(controller.IsValidModel(model, true));
        }

        /// <summary>
        /// Determines whether [is valid model not new true].
        /// </summary>
        [Test]
        public void IsValidModel_NotNew_True()
        {
            var controller = this.MockController();
            var model = new PetModel();
            model.Files = new List<FileModel>() { new FileModel() };
            model.Shelter = new ShelterModel();
            Assert.IsTrue(controller.IsValidModel(model, false));

            model.Files = new List<FileModel>();
            model.Shelter = null;
            model.Location = new Huellitas.Web.Models.Api.Common.LocationModel();
            Assert.IsTrue(controller.IsValidModel(model, false));


            model.Files = null;
            model.Shelter = null;
            model.Location = new Huellitas.Web.Models.Api.Common.LocationModel();
            Assert.IsTrue(controller.IsValidModel(model, false));
        }

        /// <summary>
        /// Determines whether [is valid model false].
        /// </summary>
        [Test]
        public void IsValidModel_New_False()
        {
            var controller = this.MockController();
            var model = new PetModel();
            model.Files = new List<FileModel>();
            model.Shelter = new ShelterModel();
            Assert.IsFalse(controller.IsValidModel(model, true));
            Assert.IsNotNull(controller.ModelState["Files"]);
            Assert.IsNull(controller.ModelState["Location"]);

            controller = this.MockController();
            model = new PetModel();
            model.Files = new List<FileModel>() { new FileModel() { Id = 1 } };
            Assert.IsFalse(controller.IsValidModel(model, true)); 
            Assert.IsNotNull(controller.ModelState["Location"]);
            Assert.IsNull(controller.ModelState["Files"]);
        }

        /// <summary>
        /// Determines whether [is valid model not new false].
        /// </summary>
        [Test]
        public void IsValidModel_NotNew_False()
        {
            var controller = this.MockController();
            var model = new PetModel();

            model = new PetModel();
            model.Files = new List<FileModel>() { new FileModel() { Id = 1 } };
            Assert.IsFalse(controller.IsValidModel(model, false));
            Assert.IsNotNull(controller.ModelState["Location"]);
            Assert.IsNull(controller.ModelState["Files"]);
        }

        /// <summary>
        /// Mocks the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private PetsController MockController()
        {
            var mockContentService = new Mock<IContentService>();
            var fileHelpers = new Mock<IFilesHelper>();
            var customTableService = new Mock<ICustomTableService>();
            var cacheManager = new Mock<ICacheManager>();
            var pictureService = new Mock<IPictureService>();
            var contentSettings = new Mock<IContentSettings>();
            var fileService = new Mock<IFileService>();

            return new PetsController(
                mockContentService.Object, 
                fileHelpers.Object, 
                cacheManager.Object, 
                customTableService.Object, 
                this.WorkContextMock.Object,
                pictureService.Object,
                contentSettings.Object,
                fileService.Object);
        }
    }
}