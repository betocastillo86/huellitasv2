//-----------------------------------------------------------------------
// <copyright file="SheltersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Collections.Generic;
    using Business.Services.Contents;
    using Data.Entities;
    using Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    /// <summary>
    /// Shelters <c>Api</c> Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class SheltersController : BaseApiController
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SheltersController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        public SheltersController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the result</returns>
        [HttpGet]
        public IActionResult Get()
        {
            ////TODO:Implementar
            var contents = this.contentService.Search(contentType: ContentType.Shelter);
            return this.Ok(contents.ToList(), contents.HasNextPage, contents.TotalCount); 
        }
    }
}
