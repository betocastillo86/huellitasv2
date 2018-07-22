using Beto.Core.Data;
using Huellitas.Business.Services;
using Huellitas.Data.Entities;

using Huellitas.Web.Controllers.Api;
using Huellitas.Web.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Tests.Web.ApiControllers.Notifications
{
    /// <summary>
    /// User Notifications Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class UserNotificationsControllerTest : BaseTest
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private Mock<INotificationService> notificationService = new Mock<INotificationService>();

        /// <summary>
        /// Gets the user notifications bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUserNotifications_BadRequest()
        {
            this.Setup();

            this.notificationService.Setup(c => c.GetUserNotifications(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<SystemNotification>(new List<SystemNotification>().AsQueryable(), 0, 10));

            var controller = this.GetController();

            var filter = new UserNotificationFilterModel();
            filter.PageSize = 500;

            var response = await controller.Get(filter) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Gets the user notifications ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUserNotifications_Ok()
        {
            this.Setup();

            this.notificationService.Setup(c => c.GetUserNotifications(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<SystemNotification>(new List<SystemNotification>().AsQueryable(), 0, 10));

            var controller = this.GetController();

            var filter = new UserNotificationFilterModel();

            var response = await controller.Get(filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.notificationService = new Mock<INotificationService>();
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private UserNotificationsController GetController()
        {
            return new UserNotificationsController(
                this.workContext.Object,
                this.notificationService.Object,
                this.messageExceptionFinder.Object);
        }
    }
}