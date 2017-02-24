using System.Threading.Tasks;
using Huellitas.Business.Security;
using Huellitas.Business.Services.AdoptionForms;
using Huellitas.Business.Services.Contents;
using Huellitas.Data.Entities;
using Huellitas.Web.Models.Api.AdoptionForms;
using Huellitas.Web.Models.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Huellitas.Web.Controllers.Api.AdoptionForms
{
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

        public AdoptionFormUsersController(
            IWorkContext workContext,
            IContentService contentService,
            IAdoptionFormService adoptionFormService) : base(workContext, contentService, adoptionFormService)
        {
            this.workContext = workContext;
            this.contentService = contentService;
            this.adoptionFormService = adoptionFormService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(int formId, [FromBody] AdoptionFormUserModel model)
        {
            ////TODO:Test
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