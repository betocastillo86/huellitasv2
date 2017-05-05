//-----------------------------------------------------------------------
// <copyright file="CommentsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    /// Comments Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/comments")]
    public class CommentsController : BaseApiController
    {
        /// <summary>
        /// The comment service
        /// </summary>
        private readonly ICommentService commentService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="commentService">The comment service.</param>
        /// <param name="workContext">The work context.</param>
        public CommentsController(
            ICommentService commentService,
            IWorkContext workContext)
        {
            this.commentService = commentService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="friendlyName">The name.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ////TODO:Test
            var comment = this.commentService.GetById(id);
            if (comment != null)
            {
                if (!comment.CanUserDeleteComment(this.workContext.CurrentUser))
                {
                    return this.Forbid();
                }

                await this.commentService.Delete(comment);

                return this.Ok(new { result = true });
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the filter</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] CommentFilterModel filter)
        {
            ////TODO:Test
            filter = filter ?? new CommentFilterModel();

            if (filter.IsValid(false))
            {
                var comments = this.commentService.Search(
                    filter.Keyword,
                    filter.OrderByEnum,
                    filter.ParentId,
                    filter.UserId,
                    filter.ContentId,
                    filter.Page,
                    filter.PageSize);

                var models = comments.ToModels(
                    this.workContext.CurrentUser,
                    this.commentService,
                    contentUrlFunction: Url.Content,
                    loadFirstComments: filter.WithChildren);

                return this.Ok(models, comments.HasNextPage, comments.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]CommentModel model)
        {
            if (this.IsValidModel(model))
            {
                var comment = model.ToEntity();
                comment.UserId = this.workContext.CurrentUserId;

                try
                {
                    await this.commentService.Insert(comment);
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }

                return this.Ok(new BaseModel() { Id = comment.Id });
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidModel(CommentModel model)
        {
            if (model == null)
            {
                return false;
            }

            if (model.Id == 0 && !model.ParentCommentId.HasValue && !model.ContentId.HasValue)
            {
                this.ModelState.AddModelError("ContentId", "Campo opcional no ingresado. Debe tener ContentID o ParentCommentID");
                this.ModelState.AddModelError("ParentCommentId", "Campo opcional no ingresado. Debe tener ContentID o ParentCommentID");
            }

            return this.ModelState.IsValid;
        }
    }
}