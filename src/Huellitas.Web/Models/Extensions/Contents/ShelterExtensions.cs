//-----------------------------------------------------------------------
// <copyright file="ShelterExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions.Contents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Files;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Api.Users;
    using Huellitas.Web.Models.Extensions.Common;

    /// <summary>
    /// Shelter Extensions
    /// </summary>
    public static class ShelterExtensions
    {
        /// <summary>
        /// To the shelter model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
        /// <param name="withRelated">if set to <c>true</c> [with related].</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <returns>the model</returns>
        public static ShelterModel ToShelterModel(
            this Content entity,
            IContentService contentService,
            IFilesHelper filesHelper = null,
            Func<string, string> contentUrlFunction = null,
            bool withFiles = false,
            bool withRelated = false,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0)
        {
            ////TODO:Test
            var model = new ShelterModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Body = entity.Body,
                CommentsCount = entity.CommentsCount,
                DisplayOrder = entity.DisplayOrder,
                Status = entity.StatusType,
                TypeId = entity.Type,
                Views = entity.Views,
                CreatedDate = entity.CreatedDate,
                Featured = entity.Featured,
                Email = entity.Email
            };

            if (entity.FileId.HasValue && entity.File != null)
            {
                model.Image = entity.File.ToModel(filesHelper, contentUrlFunction, width, height, thumbnailWidth, thumbnailHeight);
            }

            if (entity.LocationId.HasValue && entity.Location != null)
            {
                model.Location = new Api.Common.LocationModel() { Id = entity.LocationId.Value, Name = entity.Location.Name };
            }

            if (entity.User != null)
            {
                model.User = new BaseUserModel
                {
                    Id = entity.UserId,
                    Name = entity.User.Name
                };
            }

            if (withFiles && filesHelper != null)
            {
                model.Files = contentService.GetFiles(entity.Id)
                    .Select(c => c.File)
                    .ToList()
                    .ToModels(filesHelper, contentUrlFunction, width, height, thumbnailWidth, thumbnailHeight);
            }

            foreach (var attribute in entity.ContentAttributes)
            {
                var value = attribute.Value;

                switch (attribute.AttributeType)
                {
                    case ContentAttributeType.Phone1:
                        model.Phone = value;
                        break;

                    case ContentAttributeType.Phone2:
                        model.Phone2 = value;
                        break;

                    case ContentAttributeType.Lat:
                        model.Lat = Convert.ToDecimal(value);
                        break;

                    case ContentAttributeType.Lng:
                        model.Lng = Convert.ToDecimal(value);
                        break;

                    case ContentAttributeType.Owner:
                        model.Owner = value;
                        break;

                    case ContentAttributeType.Address:
                        model.Address = value;
                        break;

                    case ContentAttributeType.Facebook:
                        model.Facebook = value;
                        break;

                    case ContentAttributeType.Twitter:
                        model.Twitter = value;
                        break;

                    case ContentAttributeType.Instagram:
                        model.Instagram = value;
                        break;

                    case ContentAttributeType.Video:
                        model.Video = value;
                        break;

                    default:
                        break;
                }
            }

            return model;
        }

        /// <summary>
        /// To the shelter models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
        /// <param name="withRelated">if set to <c>true</c> [with related].</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <returns>the models</returns>
        public static IList<ShelterModel> ToShelterModels(
            this ICollection<Content> entities,
            IContentService contentService,
            IFilesHelper filesHelper = null,
            Func<string, string> contentUrlFunction = null,
            bool withFiles = false,
            bool withRelated = false,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0)
        {
            return entities
                .Select(c => c.ToShelterModel(
                    contentService,
                    filesHelper,
                    contentUrlFunction,
                    withFiles,
                    withRelated,
                    width,
                    height,
                    thumbnailWidth,
                    thumbnailHeight))
                .ToList();
        }
    }
}