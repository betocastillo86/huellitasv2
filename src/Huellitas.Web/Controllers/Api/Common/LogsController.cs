//-----------------------------------------------------------------------
// <copyright file="LogsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
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
            IWorkContext workContext,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.logService = logService;
            this.workContext = workContext;
        }

        [HttpPost]
        [RequiredModel]
        public IActionResult Post([FromBody] LogModel model)
        {
            model = model ?? new LogModel();

            if (this.IsValidModel(model))
            {
                this.logService.Error(model.ShortMessage, model.FullMessage);
                return this.Ok(new { result = true });
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
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

        private bool IsValidModel(LogModel model)
        {
            return this.ModelState.IsValid;
        }
    }
}