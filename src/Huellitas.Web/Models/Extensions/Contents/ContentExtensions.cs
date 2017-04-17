//-----------------------------------------------------------------------
// <copyright file="ContentExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Api;
    using Business.Extensions;
    using Business.Services;
    
    using Data.Extensions;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Content Extensions
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the model</returns>
        public static ContentModel ToModel(
            this Content entity,
            IFilesHelper filesHelper,
            Func<string, string> contentUrlFunction = null,
            int width = 0,
            int height = 0)
        {
            var model = new ContentModel();
            model.Id = entity.Id;
            model.Views = entity.Views;
            model.Name = entity.Name;
            model.Status = entity.StatusType;
            model.Attributes = entity.ContentAttributes.ToModels();
            model.Body = entity.Body;
            model.CommentsCount = entity.CommentsCount;
            model.CreatedDate = entity.CreatedDate;
            model.DisplayOrder = entity.DisplayOrder;
            model.Email = entity.Email;
            model.Featured = entity.Featured;

            if (entity.FileId.HasValue && entity.File != null)
            {
                model.Image = entity.File.ToModel(filesHelper, contentUrlFunction, width, height);
            }

            if (entity.LocationId.HasValue && entity.Location != null)
            {
                model.Location = entity.Location.ToModel();
            }

            if (entity.User != null)
            {
                model.User = entity.User.ToBaseUserModel();
            }

            return model;
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the list</returns>
        public static IList<ContentModel> ToModels(
            this ICollection<Content> entities,
            IFilesHelper filesHelper,
            Func<string, string> contentUrlFunction = null,
            int width = 0,
            int height = 0)
        {
            return entities
                .Select(c => c.ToModel(filesHelper, contentUrlFunction, width, height))
                .ToList();
        }

        /// <summary>
        /// Determines whether this instance [can user edit content] the specified content.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="content">The content.</param>
        /// <param name="contentService">The content service.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user edit content] the specified content; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanUserEditContent(this User user, Content content, IContentService contentService)
        {
            switch (content.Type)
            {
                case ContentType.Pet:
                    return user.CanUserEditPet(content, contentService);
                case ContentType.Shelter:
                    return user.CanUserEditShelter(content, contentService);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet] the specified content.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="content">The content.</param>
        /// <param name="contentService">The content service.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user edit pet] the specified content; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanUserEditPet(this User user, Content content, IContentService contentService)
        {
            if (!user.CanEditAnyContent())
            {
                var shelterId = content.GetAttribute<int>(ContentAttributeType.Shelter);

                if (content.UserId == user.Id)
                {
                    return true;
                }
                else if (shelterId > 0)
                {
                    ////Searches the user in shelter's users to validate if can change the pet
                    var shelterUsers = contentService.GetUsersByContentId(shelterId, Data.Entities.Enums.ContentUserRelationType.Shelter)
                                                .Select(c => c.UserId)
                                                .ToList();

                    return shelterUsers.Contains(user.Id);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Determines whether this instance [can user edit shelter] the specified content.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="content">The content.</param>
        /// <param name="contentService">The content service.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user edit shelter] the specified content; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanUserEditShelter(this User user, Content content, IContentService contentService)
        {
            ////Only can edit content if the user is admin or content's owner
            return user.CanEditAnyContent() || content.UserId == user.Id;
        }
    }
}