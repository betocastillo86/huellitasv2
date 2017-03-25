//-----------------------------------------------------------------------
// <copyright file="UserNotificationsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Notifications
{
    using System.Linq;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services.Notifications;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Notifications;
    using Huellitas.Web.Models.Extensions.Notifications;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// User Notifications Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/notifications/mine")]
    public class UserNotificationsController : BaseApiController
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationsController"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        /// <param name="notificationService">The notification service.</param>
        public UserNotificationsController(
            IWorkContext workContext,
            INotificationService notificationService)
        {
            this.workContext = workContext;
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the list</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] UserNotificationFilterModel filter)
        {
            ////TODO:Test
            filter = filter ?? new UserNotificationFilterModel();

            if (filter.IsValid())
            {
                var notifications = this.notificationService.GetUserNotifications(this.workContext.CurrentUserId, filter.Page, filter.PageSize);

                var models = notifications.ToModels();

                if (filter.UpdateSeen)
                {
                    this.notificationService.UpdateNotificationsToSeen(notifications.Select(c => c.Id).ToArray());
                }

                return this.Ok(models, notifications.HasNextPage, notifications.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }
    }
}