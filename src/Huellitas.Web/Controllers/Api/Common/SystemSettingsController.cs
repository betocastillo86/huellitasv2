//-----------------------------------------------------------------------
// <copyright file="SystemSettingsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Huellitas.Web.Controllers.Api
{
    using System.Threading.Tasks;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// System Settings Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/systemsettings")]
    public class SystemSettingsController : BaseApiController
    {
        /// <summary>
        /// The system setting service
        /// </summary>
        private readonly ISystemSettingService systemSettingService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingsController"/> class.
        /// </summary>
        /// <param name="systemSettingService">The system setting service.</param>
        /// <param name="workContext">The work context.</param>
        public SystemSettingsController(
            ISystemSettingService systemSettingService,
            IWorkContext workContext)
        {
            this.systemSettingService = systemSettingService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the list</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] SystemSettingFilterModel filter)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (filter.IsValid())
            {
                var settings = this.systemSettingService.Get(filter.Keyword, null, filter.Page, filter.PageSize);
                var models = settings.ToModels();
                return this.Ok(models, settings.HasNextPage, settings.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }

        /// <summary>
        /// Posts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Post(int id, [FromBody] SystemSettingModel model)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValid(model))
            {
                var setting = this.systemSettingService.GetByKey(model.Name);

                if (setting != null && setting.Id == id)
                {
                    setting.Value = model.Value;
                    await this.systemSettingService.Update(setting);

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
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if the specified model is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValid(SystemSettingModel model)
        {
            if (model == null)
            {
                return false;
            }

            return ModelState.IsValid;
        }
    }
}