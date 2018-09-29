//-----------------------------------------------------------------------
// <copyright file="SheltersControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Data.Files;
    using Beto.Core.Web.Api;
    using Data.Entities;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services;
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Shelters Controller Test
    /// </summary>
    [TestFixture]
    public class SheltersControllerTest : BaseTest
    {
        /// <summary>
        /// The file helpers
        /// </summary>
        private Mock<IFilesHelper> fileHelpers = new Mock<IFilesHelper>();

        /// <summary>
        /// The file service
        /// </summary>
        private Mock<IFileService> fileService = new Mock<IFileService>();

        /// <summary>
        /// The picture service
        /// </summary>
        private Mock<IPictureService> pictureService = new Mock<IPictureService>();

        /// <summary>
        /// Gets the type of the shelter by identifier bad request not pet.
        /// </summary>
        [Test]
        public void GetShelterById_BadRequest_NotPetType()
        {
            this.Setup();

            var model = new PetModel().MockNew();

            int id = 1;

            var content = new Content() { Id = id, Type = ContentType.Pet };
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), true))
                .Returns(content);

            var files = new List<ContentFile> { new ContentFile { File = new File { Id = 1, Name = "123", FileName = "123" } }, new ContentFile { File = new File { Id = 2, Name = "456", FileName = "456" } } };
            this.contentService.Setup(c => c.GetFiles(It.IsAny<int>()))
                .Returns(files);

            var controller = this.GetController();
            controller.AddUrl();

            var response = controller.Get(id.ToString()) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Gets the shelter by identifier not found.
        /// </summary>
        [Test]
        public void GetShelterById_NotFound()
        {
            this.Setup();

            var model = new ShelterModel().MockNew();

            int id = 1;

            var content = new Content() { Id = id };
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), true))
                .Returns((Content)null);

            var controller = this.GetController();

            var response = controller.Get(id.ToString()) as NotFoundResult;
            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Gets the shelter by identifier not found unpublished.
        /// </summary>
        [Test]
        public void GetShelterById_NotFound_Unpublished()
        {
            this.Setup();

            this.workContext.SetupGet(c => c.CurrentUser)
                .Returns(new User() { Id = 1, RoleEnum = RoleEnum.Public });

            int id = 1;
            var content = new Content() { Id = id, Type = ContentType.Shelter, StatusType = StatusType.Created };

            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), true))
                .Returns(content);

            var controller = this.GetController();

            var response = controller.Get(id.ToString()) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Gets the shelter by identifier ok unpublished.
        /// </summary>
        [Test]
        public void GetShelterById_Ok_Unpublished()
        {
            this.Setup();

            this.workContext.SetupGet(c => c.CurrentUser)
                .Returns(new User() { Id = 1, RoleEnum = RoleEnum.SuperAdmin });

            int id = 1;
            var content = new Content() { Id = id, Type = ContentType.Shelter, StatusType = StatusType.Created };

            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), true))
                .Returns(content);

            this.contentService.Setup(c => c.GetFiles(id))
                .Returns(new List<ContentFile>());

            var controller = this.GetController();
            controller.AddUrl();

            var response = controller.Get(id.ToString()) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Posts the shelter bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostShelter_BadRequest()
        {
            this.Setup();

            var controller = this.GetController();

            var model = new ShelterModel();
            var response = await controller.Post(model) as ObjectResult;

            var error = (response.Value as BaseApiErrorModel).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("Files", error.Details[0].Target);
            Assert.AreEqual("Location", error.Details[1].Target);
        }

        /// <summary>
        /// Posts the shelter ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostShelter_Ok()
        {
            this.Setup();

            this.fileService = new Mock<IFileService>();
            this.fileService.Setup(c => c.GetByIds(It.IsAny<int[]>()))
                .Returns(new List<File>());

            

            var model = new ShelterModel().MockNew();
            model.Type = ContentType.Shelter;

            int newId = 1;

            var content = model.ToEntity(this.contentService.Object);
            this.contentService.Setup(c => c.InsertAsync(It.IsAny<Content>()))
                .Callback((Content content1) =>
                {
                    content1.Id = newId;
                })
                .Returns(Task.FromResult(0));

            var controller = this.GetController();

            controller.AddUrl(true);

            var response = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(201, response.StatusCode);
            Assert.IsTrue(controller.IsValidModelState(model));
            Assert.AreEqual(newId, (response.Value as BaseModel).Id);
        }

        /// <summary>
        /// Puts the type of the shelters bad request not shelter.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutShelters_BadRequest_NotShelterType()
        {
            this.Setup();

            var model = new ShelterModel().MockNew();

            int newId = 1;

            var content = new Content() { Id = newId, Type = ContentType.Pet };
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false))
                .Returns(content);

            var controller = this.GetController();

            var response = await controller.Put(newId, model) as ObjectResult;
            var error = (response.Value as BaseApiErrorModel).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("BadArgument", error.Code);
            Assert.AreEqual("Id", error.Details[0].Target);
        }

        /// <summary>
        /// Puts the shelters forbid.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutShelters_Forbid()
        {
            this.Setup();

            var model = new ShelterModel().MockNew();

            int newId = 1;

            this.workContext.SetupGet(c => c.CurrentUser)
                .Returns(new User() { Id = 1, RoleEnum = RoleEnum.Public });

            var content = new Content() { Id = newId, Type = ContentType.Shelter };
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false))
                .Returns(content);

            var controller = this.GetController();

            var response = await controller.Put(newId, model);
            Assert.IsTrue(response is ForbidResult);
        }

        /// <summary>
        /// Puts the shelters not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutShelters_NotFound()
        {
            this.Setup();

            var model = new ShelterModel().MockNew();

            int newId = 1;

            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false))
                .Returns((Content)null);

            var controller = this.GetController();

            var response = await controller.Put(newId, model) as NotFoundResult;
            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Puts the shelters ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutShelters_Ok()
        {
            this.Setup();

            var model = new ShelterModel().MockNew();
            model.Type = ContentType.Shelter;
            int newId = 1;

            var content = new Content() { Id = newId, Type = ContentType.Shelter };
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false))
                .Returns(content);

            var controller = this.GetController();
            controller.AddUrl(true);
            controller.AddResponse();

            var response = await controller.Put(newId, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Determines whether [is valid model false].
        /// </summary>
        [Test]
        public void SheltersController_IsValidModel_New_False()
        {
            this.Setup();

            var controller = this.GetController();
            var model = new ShelterModel();
            model.Files = new List<FileModel>();
            Assert.IsFalse(controller.IsValidModel(model, true));
            Assert.IsNotNull(controller.ModelState["Files"]);
            Assert.IsNotNull(controller.ModelState["Location"]);

            controller = this.GetController();
            model = new ShelterModel();
            model.Files = new List<FileModel>() { new FileModel() { Id = 1 } };
            Assert.IsFalse(controller.IsValidModel(model, true));
            Assert.IsNotNull(controller.ModelState["Location"]);
            Assert.IsNull(controller.ModelState["Files"]);
        }

        /// <summary>
        /// Determines whether [is valid model true].
        /// </summary>
        [Test]
        public void SheltersController_IsValidModel_New_True()
        {
            this.Setup();

            var controller = this.GetController();
            var model = new ShelterModel();
            model.Files = new List<FileModel>() { new FileModel() };
            model.Location = new Huellitas.Web.Models.Api.LocationModel();
            Assert.IsTrue(controller.IsValidModel(model, true));
        }

        /// <summary>
        /// Determines whether [is valid model not new false].
        /// </summary>
        [Test]
        public void SheltersController_IsValidModel_NotNew_False()
        {
            this.Setup();

            var controller = this.GetController();
            var model = new ShelterModel();

            model = new ShelterModel();
            Assert.IsFalse(controller.IsValidModel(model, false));
            Assert.IsNotNull(controller.ModelState["Location"]);
            Assert.IsNull(controller.ModelState["Files"]);
        }

        /// <summary>
        /// Determines whether [is valid model not new true].
        /// </summary>
        [Test]
        public void SheltersController_IsValidModel_NotNew_True()
        {
            this.Setup();

            var controller = this.GetController();
            var model = new ShelterModel();
            model.Files = new List<FileModel>() { new FileModel() };
            model.Location = new Huellitas.Web.Models.Api.LocationModel();
            Assert.IsTrue(controller.IsValidModel(model, false));

            model.Files = new List<FileModel>();
            model.Location = new Huellitas.Web.Models.Api.LocationModel();
            Assert.IsTrue(controller.IsValidModel(model, false));

            model.Files = null;
            model.Location = new Huellitas.Web.Models.Api.LocationModel();
            Assert.IsTrue(controller.IsValidModel(model, false));
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.contentService = new Mock<IContentService>();
            this.contentSettings = new Mock<IContentSettings>();
            this.fileHelpers = new Mock<IFilesHelper>();
            this.fileService = new Mock<IFileService>();
            this.pictureService = new Mock<IPictureService>();
            base.Setup();
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private SheltersController GetController()
        {
            Mock<ILocationService> locationService = new Mock<ILocationService>();
            locationService.Setup(c => c.GetCachedLocationById(It.IsAny<int>()))
                .Returns(() => new Location { Id = 1, Name = "name" });
            Mock<ISeoService> seoService = new Mock<ISeoService>();
            Mock<IRepository<Content>> contentRepository = new Mock<IRepository<Content>>();

            return new SheltersController(
                this.contentService.Object,
                this.fileHelpers.Object,
                this.contentSettings.Object,
                this.workContext.Object,
                this.fileService.Object,
                this.pictureService.Object,
                locationService.Object,
                seoService.Object,
                contentRepository.Object,
                this.publisher.Object,
                this.messageExceptionFinder.Object);
        }
    }
}