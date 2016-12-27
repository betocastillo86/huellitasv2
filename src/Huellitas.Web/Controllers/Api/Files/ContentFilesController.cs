//-----------------------------------------------------------------------
// <copyright file="ContentFilesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Exceptions;
    using Business.Services.Contents;
    using Business.Services.Files;
    using Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Files;
    using Infraestructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Extensions.Common;

    /// <summary>
    /// Content Files Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/contents/{contentId:int}/files")]
    public class ContentFilesController : BaseApiController
    {
        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The file helper
        /// </summary>
        private readonly IFilesHelper fileHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentFilesController"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        /// <param name="fileHelper">the file helper</param>
        public ContentFilesController(
            IFileService fileService,
            IFilesHelper fileHelper)
        {
            this.fileService = fileService;
            this.fileHelper = fileHelper;
        }


        /// <summary>
        /// Gets the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <returns>The files of content</returns>
        [HttpGet]
        public IActionResult Get(int contentId, [FromQuery]FilterSizeModel sizes)
        {
            var models = this.fileService.GetByContentId(contentId)
                .ToModels(this.fileHelper, Url.Content, sizes.Width, sizes.Height);

            return this.Ok(models);
        }

        /// <summary>
        /// Posts the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the value</returns>
        [HttpPost]
        public async Task<IActionResult> Post(int contentId, [FromBody]FileModel model)
        {
            ////TODO:Solo usuarios administradores
            if (model != null && model.Id > 0)
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
                    ////TODO:Redimensionar las imagenes
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
                this.ModelState.AddModelError("Id", "El campo File Id es obligatorio");
                return this.BadRequest(this.ModelState);
            }
        }

        [HttpDelete]
        [Route("{fileId}")]
        public IActionResult Delete(int contentId, int fileId)
        {
            ////TODO:Implementar
            return this.Ok(new { result = true });
        }
    }
}