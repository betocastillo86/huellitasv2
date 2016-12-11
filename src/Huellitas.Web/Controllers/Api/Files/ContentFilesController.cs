//-----------------------------------------------------------------------
// <copyright file="ContentFilesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Files
{
    using System.Collections.Generic;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Files;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Content Files Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/contents/{contentId:int}/files")]
    public class ContentFilesController : BaseApiController
    {
        /// <summary>
        /// Gets the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <returns>The files of content</returns>
        [HttpGet]
        public IActionResult Get(int contentId)
        {
            ////TODO:Implementar metodo
            var files = new List<FileModel>();
            files.Add(new Models.Api.Files.FileModel() { Id = 1, Name = "Archivo Uno", FileName = "/img/content/000000/1_imagen1.jpg" });
            files.Add(new Models.Api.Files.FileModel() { Id = 2, Name = "Archivo Dos", FileName = "/img/content/000000/2_imagen2.jpg" });
            return this.Ok(files);
        }

        /// <summary>
        /// Posts the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the value</returns>
        [HttpPost]
        public IActionResult Post(int contentId, [FromBody]FileModel model)
        {
            ////TODO:Implementar metodo
            return this.Ok(new { Id = new System.Random().Next(5, 150) });
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