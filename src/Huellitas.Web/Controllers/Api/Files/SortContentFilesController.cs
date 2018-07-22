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
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    public class SortContentFilesController : BaseApiController
    {
        private readonly IWorkContext workContext;

        private readonly IContentService contentService;

        public SortContentFilesController(
            IWorkContext workContext,
            IContentService contentService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.contentService = contentService;
        }

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