//-----------------------------------------------------------------------
// <copyright file="ContentExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System.Linq;
    using Business.Extensions.Entities;
    using Data.Extensions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Content Extensions
    /// </summary>
    public static class ContentExtensions
    {
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