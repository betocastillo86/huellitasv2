//-----------------------------------------------------------------------
// <copyright file="SocialPostsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Files
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data.Files;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Api.Files;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Facebook Posts Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/contents/{id:int}/socialpost")]
    public class SocialPostsController : BaseApiController
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialPostsController"/> class.
        /// </summary>
        /// <param name="pictureService">The picture service.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public SocialPostsController(
            IPictureService pictureService,
            IFilesHelper filesHelper,
            IWorkContext workContext,
            IContentService contentService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.pictureService = pictureService;
            this.filesHelper = filesHelper;
            this.workContext = workContext;
            this.contentService = contentService;
        }

        /// <summary>
        /// Posts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the mdoel</param>
        /// <returns>the task</returns>
        [Authorize]
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post(int id, [FromBody] SocialPostsModel model)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            var content = this.contentService.GetById(id, true);

            if (content != null)
            {
                var file = content.File;

                if (model.FileId.HasValue)
                {
                    file = this.contentService.GetFiles(id)
                        .FirstOrDefault(c => c.FileId == model.FileId)?.File;
                }

                if (file != null)
                {
                    if (this.filesHelper.IsImageExtension(file.FileName))
                    {
                        var filePath = await this.pictureService.CreateSocialNetworkPost(content, file, model.Color, model.SocialNetwork, Url.Content);
                        return this.Ok(new FileModel() { Id = file.Id, FileName = file.Name, Thumbnail = filePath });
                    }
                    else
                    {
                        return this.BadRequest(new List<ApiErrorModel>() { new ApiErrorModel() { Code = "IsNotImage", Message = "El archivo que se desea convertir no es una imagen" } });
                    }
                }
                else
                {
                    return this.BadRequest(new List<ApiErrorModel>() { new ApiErrorModel() { Code = "ImageNotFound", Message = "El archivo que se desea convertir no pertenece al contenido", Target = "FileId" } });
                }
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}