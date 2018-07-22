namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data.Notifications;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Pet Notifications Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/pets/{id:int}/notify/{type}")]
    public class PetNotificationsController : BaseApiController
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
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The SEO service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetNotificationsController"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="seoService">The SEO service.</param>
        /// <param name="pictureService">The picture service.</param>
        /// <param name="contentSettings">The content settings.</param>
        public PetNotificationsController(
            IWorkContext workContext,
            INotificationService notificationService,
            IContentService contentService,
            ISeoService seoService,
            IPictureService pictureService,
            IContentSettings contentSettings,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.notificationService = notificationService;
            this.contentService = contentService;
            this.seoService = seoService;
            this.pictureService = pictureService;
            this.contentSettings = contentSettings;
        }

        /// <summary>
        /// Posts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post(int id, NotificationType type)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (type != NotificationType.PetRejected && type != NotificationType.PetWillBeHiddenByNotAnswer)
            {
                return this.BadRequest(new List<ApiErrorModel> { new ApiErrorModel { Code = "NotValidNotification", Message = "No se puede enviar esta notificación" } });
            }

            var pet = this.contentService.GetById(id);

            if (pet != null)
            {
                var user = pet.User;
                var targetUrl = this.seoService.GetFullRoute("pet", pet.FriendlyName);

                var parameters = new List<NotificationParameter>();
                parameters.Add("Pet.Name", pet.Name);
                parameters.Add("Pet.Url", targetUrl);
                parameters.Add("Pet.CreationDate", pet.CreatedDate.ToString("YYYY/MM/DD"));
                if (pet.File != null)
                {
                    parameters.Add("Pet.Image", this.pictureService.GetPicturePath(pet.File, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList));
                }

                await this.notificationService.NewNotification(user, null, type, targetUrl, parameters);

                return this.Ok();
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}