//-----------------------------------------------------------------------
// <copyright file="SendAdoptionFormsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Huellitas.Business.Notifications;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Text;

    /// <summary>
    /// Send Adoption Forms Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Controllers.ApiBaseController" />
    [Route("api/adoptionforms/{formId:int}/send")]
    public class SendAdoptionFormsController : AdoptionFormsBaseController
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendAdoptionFormsController"/> class.
        /// </summary>
        /// <param name="adoptionFormService">The adoption form service.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        public SendAdoptionFormsController(
            IAdoptionFormService adoptionFormService,
            INotificationService notificationService,
            IWorkContext workContext,
            IContentService contentService) : base(workContext, contentService, adoptionFormService)
        {
            this.adoptionFormService = adoptionFormService;
            this.notificationService = notificationService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Posts the specified form identifier.
        /// </summary>
        /// <param name="formId">The form identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(int formId, [FromBody] SendAdoptionFormModel model)
        {
            if (this.IsValidModel(model))
            {
                var form = this.adoptionFormService.GetById(formId);

                if (form != null)
                {
                    if (this.CanSeeForm(form))
                    {
                        var user = new User() { Name = model.Email, Email = model.Email };

                        var parameters = new List<NotificationParameter>();
                        parameters.Add("Pet.Name", form.Content.Name);
                        parameters.Add("Pet.Url", form.Content.Name);


                        var attributesHtml = new StringBuilder();

                        attributesHtml.Append($"<br>Nombre: {form.Name}");
                        attributesHtml.Append($"<br>Ubicación: {form.Location.Name}");
                        attributesHtml.Append($"<br>Dirección: {form.Address}");
                        attributesHtml.Append($"<br>Barrio: {form.Town}");
                        attributesHtml.Append($"<br>Fecha de nacimiento: {form.BirthDate}");
                        attributesHtml.Append($"<br>Número telefónico: {form.PhoneNumber}");

                        attributesHtml.Append($"<br><h2>Preguntas del formulario</h2><br><br>");

                        var attributes = this.adoptionFormService.GetAttributes(formId);
                        for (int i = 0; i < attributes.Count; i++)
                        {
                            var attribute = attributes[i];
                            attributesHtml.Append($"<br> {i + 1}. {attribute.Attribute.Value} <br><b>R/ {attribute.Value}</b>");
                        }

                        parameters.Add("Form.Attributes", attributesHtml.ToString());


                        await this.notificationService.NewNotification(
                            user,
                            this.workContext.CurrentUser,
                            NotificationType.AdoptionFormSentToOtherUser,
                            string.Empty,
                            parameters);

                        return this.Ok(new { result = true });
                    }
                    else
                    {
                        return this.Forbid();
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
        private bool IsValidModel(SendAdoptionFormModel model)
        {
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }
    }
}