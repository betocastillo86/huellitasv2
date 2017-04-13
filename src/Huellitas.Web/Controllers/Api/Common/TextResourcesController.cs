//-----------------------------------------------------------------------
// <copyright file="TextResourcesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using System.Threading.Tasks;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions.Entities;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services.Configuration;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Extensions.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Text resources controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/textresources")]
    public class TextResourcesController : BaseApiController
    {
        /// <summary>
        /// The text resource service
        /// </summary>
        private readonly ITextResourceService textResourceService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextResourcesController"/> class.
        /// </summary>
        /// <param name="textResourceService">The text resource service.</param>
        /// <param name="workContext">The work context.</param>
        public TextResourcesController(
            ITextResourceService textResourceService,
            IWorkContext workContext)
        {
            this.textResourceService = textResourceService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery]TextResourceFilterModel filter)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (filter.IsValid())
            {
                var resources = this.textResourceService
                    .GetAll(Data.Entities.Enums.LanguageEnum.Spanish, filter.Keyword, filter.Page, filter.PageSize);

                var models = resources.ToModels();

                return this.Ok(models, resources.HasNextPage, resources.TotalCount);
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
        /// <returns>the values</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]TextResourceModel model)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {
                var entity = model.ToEntity();
                entity.Language = Data.Entities.Enums.LanguageEnum.Spanish;

                try
                {
                    await this.textResourceService.Insert(entity);
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }

                return this.Ok(new BaseModel { Id = entity.Id });
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
        /// <returns>the action</returns>
        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]TextResourceModel model)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {
                var resource = this.textResourceService.GetById(id);
                if (resource != null)
                {
                    resource.Value = model.Value;
                    resource.Name = model.Name;

                    try
                    {
                        await this.textResourceService.Update(resource);
                    }
                    catch (HuellitasException e)
                    {
                        return this.BadRequest(e);
                    }

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
        private bool IsValidModel(TextResourceModel model)
        {
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }
    }
}