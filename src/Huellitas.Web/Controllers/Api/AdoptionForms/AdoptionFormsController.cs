//-----------------------------------------------------------------------
// <copyright file="AdoptionFormsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Beto.Core.Caching;
    using Beto.Core.Data.Files;
    using Business.Exceptions;
    using Business.Extensions;
    using Business.Security;
    using Business.Services;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Configuration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;
    using Models.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Adoption Forms Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class AdoptionFormsController : AdoptionFormsBaseController
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
        /// The custom table service
        /// </summary>
        private readonly ICustomTableService customTableService;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormsController"/> class.
        /// </summary>
        /// <param name="adoptionFormService">The adoption form service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="customTableService">The custom table service.</param>
        public AdoptionFormsController(
            IAdoptionFormService adoptionFormService,
            IWorkContext workContext,
            IContentService contentService,
            IFilesHelper filesHelper,
            ICustomTableService customTableService,
            ICacheManager cacheManager,
            IContentSettings contentSettings) : base(workContext, contentService, adoptionFormService)
        {
            this.adoptionFormService = adoptionFormService;
            this.workContext = workContext;
            this.contentService = contentService;
            this.filesHelper = filesHelper;
            this.customTableService = customTableService;
            this.cacheManager = cacheManager;
            this.contentSettings = contentSettings;
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
                    filter.SharedToUserId,
                    filter.ParentUserId,
                    filter.AllRelatedToUserId,
                    filter.Status,
                    filter.OrderByEnum,
                    filter.PetStatus,
                    filter.Page,
                    filter.PageSize);
                var models = forms.ToModels(this.filesHelper, Url.Content, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList);
                return this.Ok(models, forms.HasNextPage, forms.TotalCount);
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
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        [Route("{id:int}", Name = "Api_AdoptionForms_GetById")]
        public IActionResult Get(int id)
        {
            var form = this.adoptionFormService.GetById(id);

            if (form != null)
            {
                if (this.CanSeeForm(form))
                {
                    var model = form.ToModel(this.filesHelper, Url.Content);

                    model.Attributes = this.adoptionFormService.GetAttributes(id).ToModels();

                    model.Answers = this.adoptionFormService.GetAnswers(id).ToModels();

                    return this.Ok(model);
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
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool IsValidModel(AdoptionFormModel model)
        {
            if (model == null)
            {
                return false;
            }

            if (model.Attributes != null)
            {
                this.ValidateQuestions(model.Attributes);
            }
            else
            {
                this.ModelState.AddModelError("Attributes", "Debe ingresar las respuestas del formulario");
            }

            //if (model.FamilyMembers.HasValue && model.FamilyMembers.Value != model.FamilyMembersAge?.Split(new char[] { ',' }).Length)
            //{
            //    this.ModelState.AddModelError("FamilyMembersAge", "Las edades deben corresponder al número de miembros");
            //}

            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdoptionFormModel model)
        {
            if (this.IsValidModel(model))
            {
                var entity = model.ToEntity();
                entity.UserId = this.workContext.CurrentUserId;

                try
                {
                    await this.adoptionFormService.Insert(entity);
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }

                var createdUri = this.Url.Link("Api_AdoptionForms_GetById", new BaseModel() { Id = entity.Id });
                return this.Created(createdUri, new BaseModel() { Id = entity.Id });
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Validates the questions.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        [NonAction]
        public void ValidateQuestions(IList<AdoptionFormAttributeModel> attributes)
        {
            var questions = this.customTableService.GetAdoptionFormQuestions(this.cacheManager);

            foreach (var question in questions)
            {
                var attribute = attributes.FirstOrDefault(c => c.AttributeId == question.Id);

                ////If it's required and it'snot fill marks error
                if (question.Required && string.IsNullOrEmpty(attribute?.Value))
                {
                    if (question.QuestionParentId.HasValue)
                    {
                        var attributeParent = attributes.FirstOrDefault(c => c.AttributeId == question.QuestionParentId.Value);
                        if (attributeParent.Value != null && attributeParent.Value.ToLower().Equals("true"))
                        {
                            this.ModelState.AddModelError("Attributes", $"La pregunta '{question.Question}' es obligatoria");
                        }
                        else
                        {
                            attributes.Remove(attributes.FirstOrDefault(c => c.AttributeId == question.Id));
                        }
                    }
                    else
                    {
                        this.ModelState.AddModelError("Attributes", $"La pregunta '{question.Question}' es obligatoria");
                    }
                }
            }
        }
    }
}