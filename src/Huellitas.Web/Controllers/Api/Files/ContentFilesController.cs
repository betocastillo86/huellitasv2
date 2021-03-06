﻿//-----------------------------------------------------------------------
// <copyright file="ContentFilesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data.Files;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Business.Configuration;
    using Business.Exceptions;
    using Business.Security;
    using Business.Services;
    using Data.Entities;
    using Hangfire;
    using Huellitas.Business.Extensions;
    using Huellitas.Web.Infraestructure.Tasks;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Operations;
    using Microsoft.AspNetCore.Mvc;
    using Models.Extensions;

    /// <summary>
    /// Content Files Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/contents/{contentId:int}/files")]
    public class ContentFilesController : BaseApiController
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The file helper
        /// </summary>
        private readonly IFilesHelper fileHelper;

        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentFilesController"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        /// <param name="fileHelper">The file helper.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="pictureService">The picture service.</param>
        /// <param name="contentSettings">The content settings.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public ContentFilesController(
            IFileService fileService,
            IFilesHelper fileHelper,
            IContentService contentService,
            IWorkContext workContext,
            IPictureService pictureService,
            IContentSettings contentSettings,
            IMessageExceptionFinder messageExceptionFinder,
            IGeneralSettings generalSettings) : base(messageExceptionFinder)
        {
            this.fileService = fileService;
            this.fileHelper = fileHelper;
            this.contentService = contentService;
            this.workContext = workContext;
            this.pictureService = pictureService;
            this.contentSettings = contentSettings;
            this.generalSettings = generalSettings;
        }

        /// <summary>
        /// Deletes the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <returns>the action</returns>
        [HttpDelete]
        [Route("{fileId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int contentId, int fileId)
        {
            var content = this.contentService.GetById(contentId);

            if (this.workContext.CurrentUser.CanUserEditContent(content, this.contentService))
            {
                await this.fileService.DeleteContentFile(contentId, fileId, true);

                return this.Ok(new { result = true });
            }
            else
            {
                return this.Forbid();
            }
        }

        /// <summary>
        /// Gets the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="sizes">The sizes.</param>
        /// <returns>the action</returns>
        [HttpGet]
        public IActionResult Get(int contentId, [FromQuery]FilterSizeModel sizes)
        {
            var models = this.contentService.GetFiles(contentId)
                .ToModels(this.fileHelper, Url.Content, sizes.Width, sizes.Height);

            return this.Ok(models);
        }

        /// <summary>
        /// Patches the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="contentFileId">The content file identifier.</param>
        /// <param name="patchDocument">The patch document.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpPatch]
        [Route("{contentFileId:int}")]
        public IActionResult Patch(int contentId, int contentFileId, [FromBody] JsonPatchDocument<FileModel> patchDocument)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (patchDocument == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var content = this.contentService.GetById(contentId);

            var contentFile = this.contentService.GetFiles(contentId).FirstOrDefault(c => c.FileId == contentFileId);

            if (content == null || contentFile == null)
            {
                return this.NotFound();
            }

            foreach (var operation in patchDocument.Operations)
            {
                // Realiza el redimensionamiento de las imagenes con el nuevo tipo de corte
                if (operation.path.Equals("/resize") && operation.OperationType == OperationType.Replace)
                {
                    // elimina las imagenes para reemplazarlas con el corte
                    System.IO.File.Delete(this.fileHelper.GetPhysicalPath(contentFile.File, this.contentSettings.PictureSizeWidthDetail, this.contentSettings.PictureSizeHeightDetail));
                    System.IO.File.Delete(this.fileHelper.GetPhysicalPath(contentFile.File, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList));

                    // crea nuevamente las imagenes con el nuevo corte
                    this.pictureService.GetPicturePath(contentFile.File, this.contentSettings.PictureSizeWidthDetail, this.contentSettings.PictureSizeHeightDetail, true, Beto.Core.Data.Files.ResizeMode.Pad);
                    this.pictureService.GetPicturePath(contentFile.File, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList, true, Beto.Core.Data.Files.ResizeMode.Pad);
                }

                if (operation.path.Equals("/rotate") && operation.OperationType == OperationType.Replace)
                {
                    // elimina las imagenes para reemplazarlas con el corte
                    System.IO.File.Delete(this.fileHelper.GetPhysicalPath(contentFile.File, this.contentSettings.PictureSizeWidthDetail, this.contentSettings.PictureSizeHeightDetail));
                    System.IO.File.Delete(this.fileHelper.GetPhysicalPath(contentFile.File, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList));

                    // crea nuevamente las imagenes con el nuevo corte
                    this.pictureService.RotateImage(contentFile.File, this.contentSettings.PictureSizeWidthDetail, this.contentSettings.PictureSizeHeightDetail, rotateOriginal: true);
                    this.pictureService.GetPicturePath(contentFile.File, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList, true);
                }
            }

            return this.Ok();
        }

        /// <summary>
        /// Posts the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the value</returns>
        [HttpPost]
        [Authorize]
        [RequiredModel]
        public async Task<IActionResult> Post(int contentId, [FromBody]FileModel model)
        {
            if (model != null && model.Id > 0)
            {
                var content = this.contentService.GetById(contentId);

                if (content != null)
                {
                    if (this.workContext.CurrentUser.CanUserEditContent(content, this.contentService))
                    {
                        var contentFile = new ContentFile()
                        {
                            ContentId = contentId,
                            FileId = model.Id,
                            DisplayOrder = model.DisplayOrder
                        };

                        try
                        {
                            await this.fileService.InsertContentFileAsync(contentFile);

                            if (this.generalSettings.EnableHangfire)
                            {
                                BackgroundJob.Enqueue<ImageResizeTask>(c => c.ResizeContentImage(model.Id));
                            }
                        }
                        catch (HuellitasException e)
                        {
                            if (e.Code == HuellitasExceptionCode.InvalidForeignKey)
                            {
                                return this.BadRequest(e, "No se encuentra la relación");
                            }
                            else
                            {
                                throw;
                            }
                        }

                        return this.Ok(new { Id = contentFile.Id });
                    }
                    else
                    {
                        return this.Forbid();
                    }
                }
                else
                {
                    return this.NotFound();
                }
            }
            else
            {
                this.ModelState.AddModelError("Id", "El campo File Id es obligatorio");
                return this.BadRequest(this.ModelState);
            }
        }
    }
}