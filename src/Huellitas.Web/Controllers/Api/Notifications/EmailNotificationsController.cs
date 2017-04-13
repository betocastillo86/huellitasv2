//-----------------------------------------------------------------------
// <copyright file="EmailNotificationsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Notifications
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions.Entities;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services.Notifications;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Api.Notifications;
    using Huellitas.Web.Models.Extensions.Notifications;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Email Notification Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/emailnotifications")]
    public class EmailNotificationsController : BaseApiController
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
        /// Initializes a new instance of the <see cref="EmailNotificationsController"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="workContext">The work context.</param>
        public EmailNotificationsController(
            INotificationService notificationService,
            IWorkContext workContext)
        {
            this.notificationService = notificationService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            var notification = this.notificationService.GetEmailNotificationById(id);

            if (notification != null)
            {
                var model = notification.ToModel();
                return this.Ok(model);
            }
            else
            {
                return this.NotFound();
            }
        }


        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery]EmailNotificationFilterModel filter)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            filter = filter ?? new EmailNotificationFilterModel();

            if (filter.IsValid())
            {
                var notifications = this.notificationService.GetEmailNotifications(
                    filter.Sent,
                    filter.To,
                    filter.Subject,
                    null,
                    filter.Page,
                    filter.PageSize);

                var models = notifications.ToModels();
                return this.Ok(models, notifications.HasNextPage, notifications.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the task</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] EmailNotificationModel model)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {

                var entity = new EmailNotification();
                entity.To = model.To;
                entity.Subject = model.Subject;
                entity.Body = model.Body;

                await this.notificationService.InsertEmailNotification(entity);

                return this.Ok(new BaseModel() { Id = entity.Id });
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the task</returns>
        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmailNotificationModel model)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {
                var notification = this.notificationService.GetEmailNotificationById(id);

                if (notification != null)
                {
                    //// NO se puede cambiar una notificacion enviada
                    if (!notification.SentDate.HasValue)
                    {
                        notification.To = model.To;
                        notification.Body = model.Body;
                        notification.Subject = model.Subject;

                        await this.notificationService.UpdateEmailNotification(notification);

                        return this.Ok();
                    }
                    else
                    {
                        return this.BadRequest(HuellitasExceptionCode.CantUpdateEmailNotification, "No se puede modificar una notificación ya enviada");
                    }
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
        private bool IsValidModel(EmailNotificationModel model)
        {
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }
    }
}