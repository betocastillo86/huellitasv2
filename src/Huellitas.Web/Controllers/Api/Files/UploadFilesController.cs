//-----------------------------------------------------------------------
// <copyright file="UploadFilesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Files
{
    using System;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Upload Files Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/uploadfiles")]
    public class UploadFilesController : BaseApiController
    {
        /// <summary>
        /// Posts this instance.
        /// </summary>
        /// <returns>the action</returns>
        [HttpPost]
        public IActionResult Post()
        {
            var name = Guid.NewGuid().ToString();
            return this.Ok(new { fileName = name });
        }
    }
}