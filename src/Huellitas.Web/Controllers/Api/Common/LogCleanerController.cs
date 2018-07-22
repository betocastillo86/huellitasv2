//-----------------------------------------------------------------------
// <copyright file="LogCleanerController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Log Cleaner Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    public class LogCleanerController : BaseApiController
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
        /// Initializes a new instance of the <see cref="LogCleanerController"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public LogCleanerController(
            ILogService logService,
            IWorkContext workContext,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.logService = logService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Posts this instance.
        /// </summary>
        /// <returns>the action</returns>
        [Route("api/logs/clean")]
        [HttpPost]
        [Authorize]
        [RequiredModel]
        public async Task<IActionResult> Post()
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            await this.logService.Clear();

            return this.Ok();
        }
    }
}