//-----------------------------------------------------------------------
// <copyright file="CacheCleanerController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using System.Threading.Tasks;
    using Beto.Core.Caching;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Web.Infraestructure.UI;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for cleaning cache
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("cache/clear")]
    public class CacheCleanerController : BaseApiController
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The JAVASCRIPT generator
        /// </summary>
        private readonly IJavascriptConfigurationGenerator javascriptGenerator;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheCleanerController"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="javascriptGenerator">The JAVASCRIPT generator.</param>
        /// <param name="workContext">The work context.</param>
        public CacheCleanerController(
            ICacheManager cacheManager,
            IJavascriptConfigurationGenerator javascriptGenerator,
            IWorkContext workContext,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.cacheManager = cacheManager;
            this.javascriptGenerator = javascriptGenerator;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            if (this.workContext.CurrentUser.IsSuperAdmin())
            {
                this.cacheManager.Clear();
                this.javascriptGenerator.CreateJavascriptConfigurationFile();
                await Task.FromResult(0);
                return this.Ok(new { result = true });
            }
            else
            {
                return this.Forbid();
            }
        }
    }
}