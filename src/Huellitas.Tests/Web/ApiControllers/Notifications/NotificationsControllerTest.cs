namespace Huellitas.Tests.Web.ApiControllers.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Huellitas.Business.Services.Notifications;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Huellitas.Web.Controllers.Api.Notifications;
    using Huellitas.Web.Models.Api.Notifications;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Notifications Controller Test
    /// </summary>
    [TestFixture]
    public class NotificationsControllerTest : BaseTest
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private Mock<INotificationService> notificationService;

        /// <summary>
        /// Notifications the controller get forbid.
        /// </summary>
        [Test]
        public void NotificationsController_Get_Forbid()
        {
            this.Setup();
            this.SetupPublicUser();

            int notificationId = 1;

            var controller = this.GetController();

            var response = controller.Get(notificationId);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Notifications the controller get ok.
        /// </summary>
        [Test]
        public void NotificationsController_Get_Ok()
        {
            this.Setup();

            int notificationId = 1;

            this.notificationService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(this.GetEntity());

            var controller = this.GetController();

            var response = controller.Get(notificationId) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Notifications the controller get all bad request.
        /// </summary>
        [Test]
        public void NotificationsController_GetAll_BadRequest()
        {
            this.Setup();

            var controller = this.GetController();

            var filter = new NotificationFilterModel() { PageSize = 150 };

            var response = controller.Get(filter) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Notifications the controller get all forbid.
        /// </summary>
        [Test]
        public void NotificationsController_GetAll_Forbid()
        {
            this.Setup();
            this.SetupPublicUser();

            var controller = this.GetController();

            var filter = new NotificationFilterModel();

            var response = controller.Get(filter);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Notifications the controller get all ok.
        /// </summary>
        [Test]
        public void NotificationsController_GetAll_Ok()
        {
            this.Setup();

            this.notificationService.Setup(c => c.GetAll(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<Notification>(new List<Notification>().AsQueryable(), 0, 5) { });

            var controller = this.GetController();

            var filter = new NotificationFilterModel();

            var response = controller.Get(filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public async Task NotificationsController_Put_Ok()
        {
            this.Setup();

            int notificationId = 1;

            this.notificationService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(this.GetEntity());

            this.notificationService.Setup(c => c.Update(It.IsAny<Notification>()))
                .Returns(Task.FromResult(0));

            var controller = this.GetController();

            var model = this.GetModel();

            var response = await controller.Put(notificationId, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public async Task NotificationsController_Put_BadRequest()
        {
            this.Setup();

            int notificationId = 1;

            var controller = this.GetController();

            var model = this.GetModel();
            model.EmailSubject = null;

            var response = await controller.Put(notificationId, model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        [Test]
        public async Task NotificationsController_Put_Forbid()
        {
            this.Setup();
            this.SetupPublicUser(55);

            int notificationId = 1;

            var controller = this.GetController();

            var model = this.GetModel();

            var response = await controller.Put(notificationId, model);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        [Test]
        public void NotificationsController_IsValidModel_Email_False()
        {
            var model = this.GetModel();
            model.EmailSubject = null;
            var controller = this.GetController();

            Assert.IsFalse(controller.IsValidModel(model));
        }

        [Test]
        public void NotificationsController_IsValidModel_System_False()
        {
            var model = this.GetModel();
            model.SystemText = null;
            var controller = this.GetController();

            Assert.IsFalse(controller.IsValidModel(model));
        }

        [Test]
        public void NotificationsController_IsValidModel_True()
        {
            var model = this.GetModel();
            var controller = this.GetController();
            Assert.IsTrue(controller.IsValidModel(model));
        }

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        protected override void Setup()
        {
            this.notificationService = new Mock<INotificationService>();
            base.Setup();
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private NotificationsController GetController()
        {
            return new NotificationsController(
                this.notificationService.Object,
                this.workContext.Object);
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns>the entity</returns>
        private Notification GetEntity()
        {
            return new Notification()
            {
                Id = 1,
                Active = true,
                Deleted = false,
                EmailHtml = "EmailHtml",
                IsEmail = true,
                EmailSubject = "EmailSubject",
                IsSystem = true,
                Name = "Name",
                SystemText = "SystemText",
                Tags = "Tags",
                UpdateDate = DateTime.Now
            };
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        private NotificationModel GetModel()
        {
            return new NotificationModel()
            {
                Active = true,
                EmailHtml = "EmailHtml",
                EmailSubject = "EmailSubject",
                Id = 1,
                IsEmail = true,
                IsSystem = true,
                Name = "Name",
                SystemText = "SystemText",
                Tags = "Tags"
            };
        }
    }
}