//-----------------------------------------------------------------------
// <copyright file="NotificationsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Notifications
{
    using System.Threading.Tasks;
    using Huellitas.Business.Extensions.Entities;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services.Notifications;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Notifications;
    using Huellitas.Web.Models.Extensions.Notifications;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Notifications Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/notifications")]
    public class NotificationsController : BaseApiController
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsController"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="workContext">The work context.</param>
        public NotificationsController(
            INotificationService notificationService,
            IWorkContext workContext)
        {
            this.notificationService = notificationService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="filter">the filter</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] NotificationFilterModel filter)
        {
            filter = filter ?? new NotificationFilterModel();

            if (filter.IsValid())
            {
                if (!this.workContext.CurrentUser.IsSuperAdmin())
                {
                    return this.Forbid();
                }

                var notifications = this.notificationService.GetAll(filter.Name, filter.Page, filter.PageSize);

                var models = notifications.ToModels();

                return this.Ok(models, notifications.HasNextPage, notifications.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the notification</returns>
        [HttpGet]
        [Authorize]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            var notification = this.notificationService.GetById(id)
                .ToModel();

            return this.Ok(notification);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] NotificationModel model)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {
                var notification = this.notificationService.GetById(id);

                if (notification != null)
                {
                    notification.Name = model.Name;
                    notification.IsEmail = model.IsEmail;
                    notification.IsSystem = model.IsSystem;
                    notification.EmailHtml = model.EmailHtml;
                    notification.SystemText = model.SystemText;
                    notification.EmailSubject = model.EmailSubject;
                    notification.Active = model.Active;

                    await this.notificationService.Update(notification);

                    return this.Ok(new { result = true });
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
        public bool IsValidModel(NotificationModel model)
        {
            if (model == null)
            {
                return false;
            }

            if (model.IsEmail)
            {
                if (string.IsNullOrWhiteSpace(model.EmailSubject))
                {
                    this.ModelState.AddModelError("EmailSubject", "Si es tipo email debe tener asunto");
                }

                if (string.IsNullOrWhiteSpace(model.EmailHtml))
                {
                    this.ModelState.AddModelError("EmailHtml", "Si es tipo email debe tener contenido HTML");
                }
            }

            if (model.IsSystem && string.IsNullOrWhiteSpace(model.SystemText))
            {
                this.ModelState.AddModelError("SystemText", "Si es tipo sistema debe tener contenido HTML");
            }

            return this.ModelState.IsValid;
        }
    }
}