//-----------------------------------------------------------------------
// <copyright file="ContentUsersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Business.Exceptions;
    using Data.Entities;
    using Data.Extensions;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Content Users Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/contents/{contentId}/users")]
    public class ContentUsersController : BaseApiController
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentUsersController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="workContext">The work context.</param>
        public ContentUsersController(
            IContentService contentService,
            IWorkContext workContext, 
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.contentService = contentService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>the users</returns>
        [HttpGet]
        public IActionResult Get(int contentId, [FromQuery] ContentUserFilterModel filter)
        {
            filter = filter ?? new ContentUserFilterModel();

            if (filter.IsValid())
            {
                var canSeeSensitiveInfo = this.workContext.CurrentUser.IsSuperAdmin();

                var users = this.contentService.GetUsersByContentId(contentId, filter.RelationType, true, filter.Page, filter.PageSize);

                var models = users
                    .Select(c => c.User)
                    .ToModels(canSeeSensitiveInfo);

                return this.Ok(models, users.HasNextPage, users.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }

        /// <summary>
        /// Posts the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [Authorize]
        [RequiredModel]
        public async Task<IActionResult> Post(int contentId, [FromBody] ContentUserModel model)
        {
            if (this.IsValidModel(model))
            {
                var content = this.contentService.GetById(contentId);

                if (content != null)
                {
                    if (this.CanAddOrRemoveUserToContent(content))
                    {
                        var contentUser = new ContentUser()
                        {
                            ContentId = contentId,
                            UserId = model.UserId.Value,
                            RelationType = model.RelationType.Value
                        };

                        try
                        {
                            await this.contentService.InsertUser(contentUser);
                        }
                        catch (HuellitasException e)
                        {
                            return this.BadRequest(e);
                        }

                        return this.Ok(new BaseModel { Id = contentUser.Id });
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
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Deletes the specified content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>the task</returns>
        [HttpDelete]
        [Authorize]
        [Route("{userId:int}")]
        public async Task<IActionResult> Delete(int contentId, int userId)
        {
            var contentUser = this.contentService.GetContentUserById(contentId, userId);

            if (contentUser != null)
            {
                if (this.CanAddOrRemoveUserToContent(contentUser.Content))
                {
                    await this.contentService.DeleteContentUser(contentUser);
                    return this.Ok(new { result = true });
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

        /// <summary>
        /// Determines whether this instance [can add user to content] the specified model.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can add user to content] the specified model; otherwise, <c>false</c>.
        /// </returns>
        public bool CanAddOrRemoveUserToContent(Content content)
        {
            if (this.workContext.CurrentUserId == content.UserId)
            {
                return true;
            }
            else if (this.workContext.CurrentUser.IsSuperAdmin())
            {
                return true;
            }
            else if (this.contentService.IsUserInContent(this.workContext.CurrentUserId, content.Id))
            {
                return true;
            }
            else if (content.Type == ContentType.Pet)
            {
                // If the content is a pet and belongs to a shelter validates if the users is in it
                var shelter = content.GetAttribute<int?>(ContentAttributeType.Shelter);
                return shelter.HasValue ? this.contentService.IsUserInContent(this.workContext.CurrentUserId, shelter.Value, ContentUserRelationType.Shelter) : false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidModel(ContentUserModel model)
        {
            if (model == null)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(ContentUserRelationType), model.RelationType))
            {
                this.ModelState.AddModelError("RelationType", "El tipo de relación no es valida");
            }

            return this.ModelState.IsValid;
        }
    }
}