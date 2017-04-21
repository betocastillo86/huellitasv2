//-----------------------------------------------------------------------
// <copyright file="FilesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Services;
    using Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;

    /// <summary>
    /// File Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/files")]
    public class FilesController : BaseApiController
    {
        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The SEO service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesController"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="fileService">The file service.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="seoService">The SEO service.</param>
        /// <param name="pictureService">The picture service.</param>
        public FilesController(
            IHostingEnvironment hostingEnvironment,
            IFileService fileService,
            IFilesHelper filesHelper,
            ISeoService seoService,
            IPictureService pictureService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.fileService = fileService;
            this.filesHelper = filesHelper;
            this.seoService = seoService;
            this.pictureService = pictureService;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            return this.Ok(new { deleted = true });
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the file id and thumbnail Url</returns>
        [HttpPost]
        public async Task<IActionResult> Post(PostFileModel model)
        {
            if (this.IsValidModel(model.Files))
            {
                var file = new File();

                foreach (var dataFile in model.Files)
                {
                    file.Name = model.Name ?? System.IO.Path.GetFileNameWithoutExtension(dataFile.FileName);
                    file.FileName = string.Concat(this.seoService.GenerateFriendlyName(file.Name), System.IO.Path.GetExtension(dataFile.FileName));
                    file.MimeType = this.filesHelper.GetContentTypeByFileName(file.FileName);

                    using (var streamFile = dataFile.OpenReadStream())
                    {
                        var fileBinary = new byte[streamFile.Length];
                        streamFile.Read(fileBinary, 0, fileBinary.Length);
                        await this.fileService.InsertAsync(file, fileBinary);
                    }
                }

                var thumbnail = this.pictureService.GetPicturePath(file, 200, 200, true);

                return this.Ok(new { Id = file.Id, Thumbnail = thumbnail });
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified files].
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified files]; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool IsValidModel(ICollection<IFormFile> files)
        {
            if (files == null)
            {
                this.ModelState.AddModelError("File", "No se ha enviado ningún archivo");
            }
            else
            {
                if (files.Count == 0)
                {
                    this.ModelState.AddModelError("File", "No se ha enviado ningún archivo");
                }
                else if (files.Count > 1)
                {
                    this.ModelState.AddModelError("File", "No se permite más de un archivo");
                }
            }

            return this.ModelState.ErrorCount == 0;
        }
    }
}