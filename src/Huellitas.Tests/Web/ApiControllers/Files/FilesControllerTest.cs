﻿//-----------------------------------------------------------------------
// <copyright file="FilesControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data.Files;
    using Beto.Core.Web.Api;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services;
    using Huellitas.Web.Controllers.Api;
   
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Files Controller Test
    /// </summary>
    [TestFixture]
    public class FilesControllerTest : BaseTest
    {
        /// <summary>
        /// Validates if it is invalid model true.
        /// </summary>
        [Test]
        public void FilesController_IsInvalidModel_True()
        {
            var controller = this.MockController();
            var files = new List<IFormFile>();
            var file = new Mock<IFormFile>();
            files.Add(file.Object);

            Assert.IsFalse(controller.IsValidModel(null));
            Assert.IsFalse(controller.IsValidModel(files));

            files.Add(file.Object);
            Assert.IsFalse(controller.IsValidModel(files));
        }

        /// <summary>
        /// Validates if it is valid model true.
        /// </summary>
        [Test]
        public void FilesController_IsValidModel_True()
        {
            var controller = this.MockController();

            var file = new Mock<IFormFile>();
            var files = new List<IFormFile> { file.Object };

            Assert.IsTrue(controller.IsValidModel(files));
        }

        /// <summary>
        /// Validates if it post a bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task FilesController_Post_BadRequest()
        {
            var controller = this.MockController();

            var model = new PostFileModel();
            model.Name = "Usuario";

            var response = await controller.Post(model) as ObjectResult;
            var error = (response.Value as BaseApiErrorModel).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("BadArgument", error.Code);
            Assert.AreEqual("File", error.Details[0].Target);
        }

        /// <summary>
        /// Mocks the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private FilesController MockController()
        {
            var fileServiceMock = new Mock<IFileService>();
            var filesHelperMock = new Mock<IFilesHelper>();
            var hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            var seoServiceMock = new Mock<ISeoService>();
            var pictureService = new Mock<IPictureService>();
            var securitySettings = new Mock<ISecuritySettings>();

            var controller = new FilesController(
                hostingEnvironmentMock.Object,
                fileServiceMock.Object,
                filesHelperMock.Object,
                seoServiceMock.Object,
                pictureService.Object,
                securitySettings.Object,
                this.messageExceptionFinder.Object);

            return controller;
        }
    }
}