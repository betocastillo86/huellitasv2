//-----------------------------------------------------------------------
// <copyright file="LogsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/logs")]
    public class LogsController : BaseApiController
    {
        private readonly ILogService logService;

        private readonly IWorkContext workContext;

        public LogsController(
            ILogService logService,
            IWorkContext workContext)
        {
            this.logService = logService;
            this.workContext = workContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] LogFilterModel filter)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            var logs = this.logService.GetAll(filter.Keyword, filter.Page, filter.PageSize);
            var models = logs.ToModels();

            return this.Ok(models, logs.HasNextPage, logs.TotalCount);
        }
    }
}