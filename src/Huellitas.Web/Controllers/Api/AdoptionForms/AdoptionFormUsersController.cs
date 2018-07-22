//-----------------------------------------------------------------------
// <copyright file="AdoptionFormUsersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Adoption Form Users Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Controllers.ApiBaseController" />
    [Route("api/adoptionforms/{formId}/users")]
    public class AdoptionFormUsersController : AdoptionFormsBaseController
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The adoption form service
        /// </summary>
        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormUsersController"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="adoptionFormService">The adoption form service</param>
        public AdoptionFormUsersController(
            IWorkContext workContext,
            IContentService contentService,
            IAdoptionFormService adoptionFormService,
            IMessageExceptionFinder messageExceptionFinder) : base(workContext, contentService, adoptionFormService, messageExceptionFinder)
        {
            this.workContext = workContext;
            this.contentService = contentService;
            this.adoptionFormService = adoptionFormService;
        }

        /// <summary>
        /// Posts the specified form identifier.
        /// </summary>
        /// <param name="formId">The form identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the task</returns>
        [Authorize]
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post(int formId, [FromBody] AdoptionFormUserModel model)
        {
            if (this.IsValidModel(model))
            {
                var form = this.adoptionFormService.GetById(formId);

                if (form != null)
                {
                    if (this.CanSeeForm(form))
                    {
                        var entity = new AdoptionFormUser();
                        entity.UserId = model.UserId;
                        entity.AdoptionFormId = formId;

                        await this.adoptionFormService.InsertUser(entity);

                        return this.Ok(new BaseModel { Id = entity.Id });
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
        [NonAction]
        private bool IsValidModel(AdoptionFormUserModel model)
        {
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }
    }
}