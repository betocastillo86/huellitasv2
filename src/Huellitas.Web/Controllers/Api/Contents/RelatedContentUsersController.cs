//-----------------------------------------------------------------------
// <copyright file="RelatedContentUsersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Linq;
    using Beto.Core.Data.Files;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Users related to a content
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/users/{id:int}/contents")]
    public class RelatedContentUsersController : BaseApiController
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
        /// Initializes a new instance of the <see cref="RelatedContentUsersController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">The files helper.</param>
        public RelatedContentUsersController(
            IContentService contentService,
            IFilesHelper filesHelper)
        {
            this.contentService = contentService;
            this.filesHelper = filesHelper;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        public IActionResult Get(int id, [FromQuery]ContentUserFilterModel filter)
        {
            if (filter.IsValid())
            {
                var contents = this.contentService.GetContentsByUserId(id, filter.RelationType, true, filter.Page, filter.PageSize);
                var models = contents
                    .Select(c => c.Content)
                    .ToModels(this.filesHelper);

                return this.Ok(models, contents.HasNextPage, contents.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }
    }
}