//-----------------------------------------------------------------------
// <copyright file="SendAdoptionFormsControllerTest.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.AdoptionForms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Huellitas.Business.Services.AdoptionForms;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Notifications;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Controllers.Api.AdoptionForms;
    using Huellitas.Web.Models.Api.AdoptionForms;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Send Adoption Form Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class SendAdoptionFormsControllerTest : BaseTest
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private Mock<IAdoptionFormService> adoptionFormService = new Mock<IAdoptionFormService>();

        /// <summary>
        /// The notification service
        /// </summary>
        private Mock<INotificationService> notificationService = new Mock<INotificationService>();

        /// <summary>
        /// Tests Post the send adoption form ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostSendAdoptionForm_Ok()
        {
            int formId = 1;

            this.Setup();

            var model = this.GetModel();
            var entity = this.GetAdoptionForm();

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(entity);

            var controller = this.GetController();

            var response = await controller.Post(formId, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Posts the send adoption form bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostSendAdoptionForm_BadRequest()
        {
            int formId = 1;

            this.Setup();

            SendAdoptionFormModel model = null;

            var controller = this.GetController();

            var response = await controller.Post(formId, model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Posts the send adoption form not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostSendAdoptionForm_NotFound()
        {
            int formId = 1;

            this.Setup();

            var model = this.GetModel();

            var entity = this.GetAdoptionForm();
            entity = null;

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(entity);

            var controller = this.GetController();

            var response = await controller.Post(formId, model) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Posts the send adoption form forbid.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostSendAdoptionForm_Forbid()
        {
            int formId = 1;

            this.Setup();
            this.SetupPublicUser(55);

            var model = this.GetModel();
            var entity = this.GetAdoptionForm();

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(entity);

            var controller = this.GetController();

            var response = await controller.Post(formId, model);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.adoptionFormService = new Mock<IAdoptionFormService>();
            this.notificationService = new Mock<INotificationService>();
            this.contentService = new Mock<IContentService>();
            base.Setup();
        }

        /// <summary>
        /// Gets the adoption form.
        /// </summary>
        /// <returns>the form</returns>
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
        private SendAdoptionFormsController GetController()
        {
            return new SendAdoptionFormsController(
                this.adoptionFormService.Object,
                this.notificationService.Object,
                this.workContext.Object,
                this.contentService.Object);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        private SendAdoptionFormModel GetModel()
        {
            return new SendAdoptionFormModel()
            {
                Email = "email@email.com"
            };
        }
    }
}