//-----------------------------------------------------------------------
// <copyright file="SortContentFilesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Huellitas.Web.Controllers.Api.Files
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Sort Content Files Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    public class SortContentFilesController : BaseApiController
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortContentFilesController"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public SortContentFilesController(
            IWorkContext workContext,
            IContentService contentService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.contentService = contentService;
        }

        /// <summary>
        /// Sorts the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="fileIdFrom">The file identifier from.</param>
        /// <param name="fileIdTo">The file identifier to.</param>
        /// <returns>the return</returns>
        [HttpPost]
        [Authorize]
        [Route("api/contents/{contentId:int}/files/{fileIdFrom:int}/sort/{fileIdTo:int}")]
        public async Task<IActionResult> Sort(int contentId, int fileIdFrom, int fileIdTo)
        {
            ////TODO:test
            var content = this.contentService.GetById(contentId);

            if (content == null)
            {
                return this.NotFound();
            }

            if (!this.workContext.CurrentUser.CanUserEditPet(content, this.contentService))
            {
                return this.Forbid();
            }

            if (content != null)
            {
                try
                {
                    await this.contentService.SortFiles(content.Id, fileIdFrom, fileIdTo);
                    return this.Ok(new { result = true });
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}