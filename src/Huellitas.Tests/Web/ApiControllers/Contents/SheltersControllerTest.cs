//-----------------------------------------------------------------------
// <copyright file="SheltersControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System.Collections.Generic;
    using Data.Entities;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Files;
    using Huellitas.Web.Controllers.Api.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Contents;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Shelters Controller Test
    /// </summary>
    [TestFixture]
    public class SheltersControllerTest
    {
        /// <summary>
        /// The content service
        /// </summary>
        private Mock<IContentService> contentService = new Mock<IContentService>();

        /// <summary>
        /// The content settings
        /// </summary>
        private Mock<IContentSettings> contentSettings = new Mock<IContentSettings>();

        /// <summary>
        /// The file helpers
        /// </summary>
        private Mock<IFilesHelper> fileHelpers = new Mock<IFilesHelper>();

        /// <summary>
        /// Gets the type of the pet by identifier bad request not pet.
        /// </summary>
        [Test]
        public void GetShelterById_BadRequest_NotPetType()
        {
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

            var response = controller.Get(id) as ObjectResult;
            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("BadArgument", error.Code);
            Assert.AreEqual("Id", error.Details[0].Target);
        }

        /// <summary>
        /// Gets the pet by identifier not found.
        /// </summary>
        [Test]
        public void GetShelterById_NotFound()
        {
            var model = new ShelterModel().MockNew();

            int id = 1;

            var content = new Content() { Id = id };
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), true))
                .Returns((Content)null);

            var controller = this.GetController();

            var response = controller.Get(id) as NotFoundResult;
            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private SheltersController GetController()
        {
            return new SheltersController(
                this.contentService.Object,
                this.fileHelpers.Object,
                this.contentSettings.Object);
        }
    }
}