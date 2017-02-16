//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.AdoptionForms
{
    using System.Threading.Tasks;
    using Business.Exceptions;
    using Business.Extensions.Entities;
    using Business.Services.AdoptionForms;
    using Business.Services.Contents;
    using Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Infraestructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.AdoptionForms;
    using Models.Api.Common;
    using Models.Extensions.AdoptionForms;

    /// <summary>
    /// Adoption Form Answers Controller
    /// </summary>
    /// <seealso cref="BaseApiController" />
    [Route("api/adoptionforms/{formId:int}/answers")]
    public class AdoptionFormAnswersController : BaseApiController
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormAnswersController"/> class.
        /// </summary>
        /// <param name="adoptionFormService">The adoption form service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        public AdoptionFormAnswersController(
            IAdoptionFormService adoptionFormService,
            IWorkContext workContext,
            IContentService contentService)
        {
            this.adoptionFormService = adoptionFormService;
            this.workContext = workContext;
            this.contentService = contentService;
        }

        [HttpGet]
        public IActionResult Get(int formId)
        {
            ////TODO:Test
            var form = this.adoptionFormService.GetById(formId);

            if (form != null)
            {
                if (this.CanUserAnswerForm(form))
                {
                    var answers = this.adoptionFormService.GetAnswers(formId);
                    var models = answers.ToModels();
                    return this.Ok(models);
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

        /// <summary>
        /// Determines whether this instance [can answer form] the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can answer form] the specified form; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool CanUserAnswerForm(AdoptionForm form)
        {
            ////TODO:Test
            ////Si es admin puede responder
            if (this.workContext.CurrentUser.IsSuperAdmin())
            {
                return true;
            }
            else if (this.workContext.CurrentUserId == form.Content.UserId)
            {
                ////Si es el dueño de la mascota puede responder
                return true;
            }
            else if (this.contentService.IsUserInContent(this.workContext.CurrentUserId, this.contentService.GetContentAttribute<int>(form.ContentId, ContentAttributeType.Shelter), Data.Entities.Enums.ContentUserRelationType.Shelter))
            {
                ////Si pertenece a la fundación puede responder
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool IsValidModel(AdoptionFormAnswerModel model)
        {
            ////TODO:Test
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="formId">the form adoption identifier</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(int formId, [FromBody] AdoptionFormAnswerModel model)
        {
            ////TODO:Test
            if (this.IsValidModel(model))
            {
                var form = this.adoptionFormService.GetById(formId);

                if (form != null)
                {
                    if (this.CanUserAnswerForm(form))
                    {
                        var answer = model.ToEntity();
                        answer.UserId = this.workContext.CurrentUserId;
                        answer.AdoptionFormId = formId;

                        try
                        {
                            await this.adoptionFormService.InsertAnswer(answer);
                            return this.Ok(new BaseModel() { Id = answer.Id });
                        }
                        catch (HuellitasException e)
                        {
                            return this.BadRequest(e);
                        }
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
    }
}