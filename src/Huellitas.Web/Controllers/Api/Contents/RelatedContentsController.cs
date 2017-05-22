//-----------------------------------------------------------------------
// <copyright file="RelatedContentsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Caching;
    using Business.Configuration;
    using Business.Exceptions;
    using Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Mvc;
    using Models.Extensions;
    using Huellitas.Business.Security;

    /// <summary>
    /// Related Contents Controller
    /// </summary>
    [Route("api/contents/{id}/related")]
    public class RelatedContentsController : BaseApiController
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The custom table service
        /// </summary>
        private readonly ICustomTableService customTableService;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedContentsController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="customTableService">The custom table service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentSettings">The content settings.</param>
        public RelatedContentsController(
            IContentService contentService,
            ICustomTableService customTableService,
            ICacheManager cacheManager,
            IFilesHelper filesHelper,
            IContentSettings contentSettings,
            IWorkContext workContext)
        {
            this.contentService = contentService;
            this.customTableService = customTableService;
            this.cacheManager = cacheManager;
            this.filesHelper = filesHelper;
            this.contentSettings = contentSettings;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        public IActionResult Get(int id, [FromQuery]RelatedContentFilterModel filter)
        {
            if (filter.IsValid())
            {
                var related = this.contentService
                    .GetRelated(id, filter.RelationType, filter.Page, filter.PageSize);

                ////Validates if the model has to be like Content type selected (Pet, shelter, etc) or just ContentModel
                if (filter.AsContentType)
                {
                    switch (filter.RelationType.Value)
                    {
                        ////when case is similar pets returns PetModel
                        case Data.Entities.RelationType.SimilarPets:
                            var models = related.ToPetModels(
                                this.contentService, 
                                this.customTableService, 
                                this.cacheManager,
                                this.workContext,
                                this.filesHelper, 
                                Url.Content,
                                width: this.contentSettings.PictureSizeWidthDetail,
                                height: this.contentSettings.PictureSizeHeightDetail,
                                thumbnailWidth: this.contentSettings.PictureSizeWidthList,
                                thumbnailHeight: this.contentSettings.PictureSizeHeightList);
                            return this.Ok(models, related.HasNextPage, related.TotalCount);
                        default:
                            return this.BadRequest("Tipo de relación inexistente");
                    }
                }
                else
                {
                   var models = related.ToModels(
                       this.filesHelper,
                        Url.Content,
                        width: this.contentSettings.PictureSizeWidthList,
                        height: this.contentSettings.PictureSizeHeightList);
                    return this.Ok(models, related.HasNextPage, related.TotalCount);
                }
            }
            else
            {
                return this.BadRequest(HuellitasExceptionCode.BadArgument, filter.Errors);
            }
        }
    }
}