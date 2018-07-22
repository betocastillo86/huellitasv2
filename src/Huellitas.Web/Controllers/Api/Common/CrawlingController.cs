using System.Threading.Tasks;
using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Beto.Core.Web.Api.Filters;
using Huellitas.Business.Extensions;
using Huellitas.Business.Security;
using Huellitas.Business.Services;
using Huellitas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Huellitas.Web.Controllers.Api.Common
{
    /// <summary>
    /// Crawling Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/crawlings")]
    public class CrawlingController : BaseApiController
    {
        /// <summary>
        /// The crawling service
        /// </summary>
        private readonly ICrawlingService crawlingService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlingController"/> class.
        /// </summary>
        /// <param name="crawlingService">The crawling service.</param>
        /// <param name="workContext">The work context.</param>
        public CrawlingController(
            ICrawlingService crawlingService,
            IWorkContext workContext,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.crawlingService = crawlingService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [Authorize]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] CrawlingModel model)
        {
            if (!this.IsValid(model))
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            var crawling = await this.crawlingService.GetByUrlAsync(model.Url);

            if (crawling == null)
            {
                crawling = new Data.SeoCrawling
                {
                    Url = model.Url,
                    Html = model.Html
                };

                await this.crawlingService.InsertAsync(crawling);
            }
            else
            {
                crawling.Html = model.Html;

                await this.crawlingService.UpdateAsync(crawling);
            }

            return this.Ok();
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if the specified model is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValid(CrawlingModel model)
        {
            if (model == null)
            {
                return false;
            }

            return ModelState.IsValid;
        }
    }
}