//-----------------------------------------------------------------------
// <copyright file="AdoptionFormUsersControllerTest.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.AdoptionForms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Huellitas.Business.Services;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Adoption Form Users Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class AdoptionFormUsersControllerTest : BaseTest
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private Mock<IAdoptionFormService> adoptionFormService = new Mock<IAdoptionFormService>();

        /// <summary>
        /// Posts the adoption form user bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormUser_BadRequest()
        {
            this.Setup();

            AdoptionFormUserModel model = null;

            var controller = this.GetController();

            var response = await controller.Post(1, model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Tests Post the adoption form user forbid.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormUser_Forbid()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var model = this.GetModel();

            this.adoptionFormService.Setup(c => c.GetById(1))
                .Returns(this.GetAdoptionForm());

            this.adoptionFormService.Setup(c => c.InsertUser(It.IsAny<AdoptionFormUser>()))
                .Callback((AdoptionFormUser c) => { c.Id = 1; })
                .Returns(Task.FromResult(0));

            var controller = this.GetController();

            var response = await controller.Post(1, model);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Posts the adoption form user not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormUser_NotFound()
        {
            this.Setup();

            var model = this.GetModel();

            this.adoptionFormService.Setup(c => c.GetById(1))
                .Returns((AdoptionForm)null);

            var controller = this.GetController();

            var response = await controller.Post(1, model) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Posts the adoption form user ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormUser_Ok()
        {
            this.Setup();

            var model = this.GetModel();

            this.adoptionFormService.Setup(c => c.GetById(1))
                .Returns(this.GetAdoptionForm());

            this.adoptionFormService.Setup(c => c.InsertUser(It.IsAny<AdoptionFormUser>()))
                .Callback((AdoptionFormUser c) => { c.Id = 1; })
                .Returns(Task.FromResult(0));

            var controller = this.GetController();

            var response = await controller.Post(1, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.contentService = new Mock<IContentService>();
            this.adoptionFormService = new Mock<IAdoptionFormService>();

            base.Setup();
        }

        /// <summary>
        /// Gets the adoption form.
        /// </summary>
        /// <returns>the model</returns>
        private AdoptionForm GetAdoptionForm()
        {
            return new AdoptionForm()
            {
                Id = 1,
                Address = "cr 10 10 10",
                Attributes = new List<AdoptionFormAttribute>() { },
                BirthDate = DateTime.Now,
                ContentId = 1,
                CreationDate = DateTime.Now,
                Email = "email@email.com",
                FamilyMembers = 1,
                FamilyMembersAge = "20",
                JobId = 1,
                LastStatusEnum = AdoptionFormAnswerStatus.None,
                LocationId = 1,
                Name = "Username",
                Town = "Barrio",
                UserId = 1,
                Content = new Content() { UserId = 1 }
            };
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private AdoptionFormUsersController GetController()
        {
            return new AdoptionFormUsersController(
                this.workContext.Object,
                this.contentService.Object,
                this.adoptionFormService.Object);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        private AdoptionFormUserModel GetModel()
        {
            return new AdoptionFormUserModel()
            {
                UserId = 55
            };
        }
    }
}