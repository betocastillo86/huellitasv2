//-----------------------------------------------------------------------
// <copyright file="UserNotificationsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
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
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public UserNotificationsController(
            IWorkContext workContext,
            INotificationService notificationService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
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
        public async Task<IActionResult> Get([FromQuery] UserNotificationFilterModel filter)
        {
            filter = filter ?? new UserNotificationFilterModel();

            if (filter.IsValid())
            {
                var notifications = this.notificationService.GetUserNotifications(this.workContext.CurrentUserId, filter.Page, filter.PageSize);

                var models = notifications.ToModels();

                if (filter.UpdateSeen)
                {
                    await this.notificationService.UpdateNotificationsToSeen(notifications.Select(c => c.Id).ToArray());
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