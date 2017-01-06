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
    using Api.Files;
    using Data.Extensions;
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
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="files">The files.</param>
        /// <returns>the content</returns>
        public static Content ToEntity(
            this ShelterModel model,
            IContentService contentService,
            Content entity = null,
            IList<FileModel> files = null)
        {
            if (entity == null)
            {
                entity = new Content();
                entity.StatusType = StatusType.Created;
                entity.Type = ContentType.Shelter;
                entity.Featured = model.Featured;

                for (int i = 0; i < model.Files.Count; i++)
                {
                    if (i == 0)
                    {
                        entity.FileId = model.Files[i].Id;
                    }

                    entity.ContentFiles.Add(new ContentFile()
                    {
                        FileId = model.Files[i].Id,
                        DisplayOrder = i
                    });
                }
            }
            else
            {
                entity.FileId = model.Files.FirstOrDefault().Id;
            }

            entity.Name = model.Name;
            entity.Body = model.Body;
            entity.DisplayOrder = model.DisplayOrder;
            entity.LocationId = model.Location.Id;
            entity.Email = model.Email;

            entity.ContentAttributes.Add(ContentAttributeType.Facebook, model.Facebook, true);
            entity.ContentAttributes.Add(ContentAttributeType.Twitter, model.Twitter, true);
            entity.ContentAttributes.Add(ContentAttributeType.Instagram, model.Instagram, true);
            entity.ContentAttributes.Add(ContentAttributeType.AutoReply, model.AutoReply, true);
            entity.ContentAttributes.Add(ContentAttributeType.Video, model.Video, true);
            entity.ContentAttributes.Add(ContentAttributeType.Address, model.Address, true);
            entity.ContentAttributes.Add(ContentAttributeType.Owner, model.Owner, true);
            entity.ContentAttributes.Add(ContentAttributeType.Lng, model.Lng, true);
            entity.ContentAttributes.Add(ContentAttributeType.Lat, model.Lat, true);
            entity.ContentAttributes.Add(ContentAttributeType.Phone2, model.Phone2, true);
            entity.ContentAttributes.Add(ContentAttributeType.Phone1, model.Phone, true);

            return entity;
        }

        /// <summary>
        /// To the shelter model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
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
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0)
        {
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

            if (entity.FileId.HasValue && entity.File != null && filesHelper != null)
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

                    case ContentAttributeType.AutoReply:
                        model.AutoReply = Convert.ToBoolean(value);
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
                    width,
                    height,
                    thumbnailWidth,
                    thumbnailHeight))
                .ToList();
        }
    }
}