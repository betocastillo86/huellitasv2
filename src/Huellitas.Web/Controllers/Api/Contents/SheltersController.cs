//-----------------------------------------------------------------------
// <copyright file="SheltersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Linq;
    using Business.Configuration;
    using Business.Exceptions;
    using Business.Services.Contents;
    using Business.Services.Files;
    using Data.Entities;
    using Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.Contents;
    using Models.Extensions.Contents;

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
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SheltersController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">files helper</param>
        public SheltersController(
            IContentService contentService,
            IFilesHelper filesHelper,
            IContentSettings contentSettings)
        {
            this.contentService = contentService;
            this.filesHelper = filesHelper;
            this.contentSettings = contentSettings;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the result</returns>
        [HttpGet]
        public IActionResult Get([FromQuery]ShelterFilterModel filter)
        {
            if (filter.IsValid())
            {
                var contents = this.contentService.Search(
                    contentType: ContentType.Shelter,
                    page: filter.Page,
                    pageSize: filter.PageSize,
                    keyword: filter.Keyword,
                    orderBy: filter.OrderByEnum,
                    locationId: filter.LocationId);

                var models = contents.ToShelterModels(
                    this.contentService, 
                    this.filesHelper, 
                    Url.Content, 
                    withFiles: false,
                    width: this.contentSettings.PictureSizeWidthDetail,
                    height: this.contentSettings.PictureSizeHeightDetail,
                    thumbnailWidth: this.contentSettings.PictureSizeWidthList,
                    thumbnailHeight: this.contentSettings.PictureSizeHeightList);

                return this.Ok(models.ToList(), contents.HasNextPage, contents.TotalCount);
            }
            else
            {
                return this.BadRequest(HuellitasExceptionCode.BadArgument, filter.Errors);
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}", Name = "Api_Shelters_GetById")]
        public IActionResult Get(int id)
        {
            var content = this.contentService.GetById(id, true);

            if (content != null)
            {
                if (content.Type == ContentType.Shelter)
                {
                    var model = content.ToShelterModel(
                    this.contentService,
                    this.filesHelper,
                    Url.Content,
                    true,
                    true,
                    this.contentSettings.PictureSizeWidthDetail,
                    this.contentSettings.PictureSizeHeightDetail,
                    this.contentSettings.PictureSizeWidthList,
                    this.contentSettings.PictureSizeHeightList);

                    return this.Ok(model);
                }
                else
                {
                    ////TODO:Test
                    this.ModelState.AddModelError("Id", "Este id no pertenece a un refugio");
                    return this.BadRequest(this.ModelState);
                }
            }
            else
            {
                ////TODO:test
                return this.NotFound();
            }
        }
    }
}