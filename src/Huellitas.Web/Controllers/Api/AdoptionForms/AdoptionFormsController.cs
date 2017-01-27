//-----------------------------------------------------------------------
// <copyright file="AdoptionFormsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.AdoptionForms
{
    using Business.Extensions.Entities;
    using Business.Services.AdoptionForms;
    using Business.Services.Contents;
    using Business.Services.Files;
    using Huellitas.Web.Infraestructure.WebApi;
    using Infraestructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.AdoptionForms;
    using Models.Extensions.AdoptionForms;

    /// <summary>
    /// Adoption Forms Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class AdoptionFormsController : BaseApiController
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormsController"/> class.
        /// </summary>
        /// <param name="adoptionFormService">The adoption form service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">The files helper.</param>
        public AdoptionFormsController(
            IAdoptionFormService adoptionFormService,
            IWorkContext workContext,
            IContentService contentService,
            IFilesHelper filesHelper)
        {
            this.adoptionFormService = adoptionFormService;
            this.workContext = workContext;
            this.contentService = contentService;
            this.filesHelper = filesHelper;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] AdoptionFormFilterModel filter)
        {
            ////TODO:Test
            ////TODO:Async
            var canSeeAllForms = this.workContext.CurrentUser.IsSuperAdmin();

            if (filter.IsValid(this.workContext.CurrentUserId, this.contentService, canSeeAllForms))
            {
                var forms = this.adoptionFormService.GetAll(
                    filter.UserName,
                    filter.PetId,
                    filter.LocationId,
                    filter.ShelterId,
                    filter.FormUserId,
                    filter.ContentUserId,
                    filter.Status,
                    filter.OrderByEnum,
                    filter.Page,
                    filter.PageSize);
                var models = forms.ToModel(this.filesHelper, Url.Content);
                return this.Ok(models, forms.HasNextPage, forms.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }
    }
}