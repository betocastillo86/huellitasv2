//-----------------------------------------------------------------------
// <copyright file="ContactUsersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data.Notifications;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contacts a specific user
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/users/{id:int}/contact")]
    public class ContactUsersController : BaseApiController
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The notification settings
        /// </summary>
        private readonly INotificationSettings notificationSettings;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsersController"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="notificationSettings">The notification settings.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public ContactUsersController(
            INotificationService notificationService,
            IWorkContext workContext,
            IUserService userService,
            INotificationSettings notificationSettings,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.notificationService = notificationService;
            this.workContext = workContext;
            this.userService = userService;
            this.notificationSettings = notificationSettings;
        }

        /// <summary>
        /// Posts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [Authorize]
        [RequiredModel]
        public async Task<IActionResult> Post(int id, [FromBody]ContactUserModel model)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {
                var user = this.userService.GetById(id);

                if (user != null)
                {
                    await this.notificationService.NewNotification(
                        user,
                        null,
                        Data.Entities.NotificationType.Manual,
                        string.Empty,
                        new List<NotificationParameter>(),
                        this.notificationSettings.EmailSenderName,
                        model.Subject,
                        model.Message);

                    return this.Ok();
                }
                else
                {
                    return this.NotFound();
                }
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidModel(ContactUserModel model)
        {
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }
    }
}