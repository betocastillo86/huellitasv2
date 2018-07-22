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

    /// <summary>
    /// Logs Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/logs")]
    public class LogsController : BaseApiController
    {
        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogsController"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public LogsController(
            ILogService logService,
            IWorkContext workContext,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.logService = logService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
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

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the return</returns>
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

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidModel(LogModel model)
        {
            return this.ModelState.IsValid;
        }
    }
}