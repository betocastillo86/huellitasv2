//-----------------------------------------------------------------------
// <copyright file="PetExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Api.Files;
    using Api.Users;
    using Business.Caching;
    using Business.Configuration;
    using Business.Exceptions;
    using Business.Extensions.Entities;
    using Business.Extensions.Services;
    using Business.Services.Common;
    using Business.Services.Contents;
    using Business.Services.Files;
    using Common;
    using Data.Entities.Enums;
    using Data.Extensions;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Contents;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Pet Extensions
    /// </summary>
    public static class PetExtensions
    {
        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="contentSettings">content settings</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="entity">entity to assign the data</param>
        /// <param name="files">The files</param>
        /// <returns>the value</returns>
        /// <exception cref="HuellitasException">the exceptions</exception>
        public static Content ToEntity(
            this PetModel model, 
            IContentSettings contentSettings,
            IContentService contentService, 
            bool isAdmin,
            Content entity = null, 
            IList<FileModel> files = null)
        {
            if (entity == null)
            {
                entity = new Content();
                entity.StatusType = StatusType.Created;
                entity.Type = ContentType.Pet;

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
            entity.Email = model.Email;
            entity.ClosingDate = model.ClosingDate;

            if (model.Shelter == null || model.Shelter.Id == 0)
            {
                entity.LocationId = model.Location.Id;
                entity.ContentAttributes.Remove(ContentAttributeType.Shelter);

                //// Cuando no tiene shelter le da solo unos días de publicacion
                if (entity.Id == 0 && !isAdmin)
                {
                    ////TODO:Test again
                    entity.ClosingDate = DateTime.Now.AddDays(contentSettings.DaysToAutoClosingPet);
                }
            }
            else
            {
                var shelter = contentService.GetById(model.Shelter.Id);
                if (shelter != null)
                {
                    entity.LocationId = shelter.LocationId;
                    entity.ContentAttributes.Add(ContentAttributeType.Shelter, shelter.Id, true);
                }
                else
                {
                    throw new HuellitasException(HuellitasExceptionCode.ShelterNotFound);
                }
            }

            if (model.Parents?.Count > 0)
            {
                foreach (var parent in model.Parents)
                {
                    entity.Users.Add(new Data.Entities.ContentUser { UserId = parent.UserId.Value, RelationType = ContentUserRelationType.Parent });
                }
            }

            entity.ContentAttributes.Add(ContentAttributeType.AutoReply, model.AutoReply, true);
            entity.ContentAttributes.Add(ContentAttributeType.Age, model.Months, true);
            entity.ContentAttributes.Add(ContentAttributeType.Subtype, model.Subtype.Value, true);
            entity.ContentAttributes.Add(ContentAttributeType.Genre, model.Genre.Value, true);
            entity.ContentAttributes.Add(ContentAttributeType.Size, model.Size.Value, true);
            entity.ContentAttributes.Add(ContentAttributeType.Castrated, model.Castrated, true);

            return entity;
        }

        /// <summary>
        /// To the pet model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="customTableService">The custom table service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
        /// <param name="withRelated">if set to <c>true</c> [with related].</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <returns>the model</returns>
        public static PetModel ToPetModel(
            this Content entity, 
            IContentService contentService, 
            ICustomTableService customTableService, 
            ICacheManager cacheManager, 
            IFilesHelper filesHelper = null, 
            Func<string, string> contentUrlFunction = null, 
            bool withFiles = false, 
            bool withRelated = false,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0)
        {
            var model = new PetModel()
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
                ClosingDate = entity.ClosingDate
            };

            if (entity.LocationId.HasValue && entity.Location != null)
            {
                model.Location = new Api.Common.LocationModel() { Id = entity.LocationId.Value, Name = entity.Location.Name };
            }

            if (entity.FileId.HasValue && entity.File != null)
            {
                model.Image = entity.File.ToModel(filesHelper, contentUrlFunction, width, height, thumbnailWidth, thumbnailHeight);
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

            if (withRelated)
            {
                model.RelatedPets = contentService.GetRelated(entity.Id, Data.Entities.Enums.RelationType.SimilarPets)
                    .ToPetModels(contentService, customTableService, cacheManager, filesHelper, contentUrlFunction, false);
            }

            foreach (var attribute in entity.ContentAttributes)
            {
                var attributeId = 0;
                int.TryParse(attribute.Value, out attributeId);

                switch (attribute.AttributeType)
                {
                    case ContentAttributeType.Subtype:
                        model.Subtype = new ContentAttributeModel<int>() { Text = customTableService.GetValueByCustomTableAndId(CustomTableType.AnimalSubtype, attributeId), Value = attributeId };
                        break;

                    case ContentAttributeType.Genre:
                        model.Genre = new ContentAttributeModel<int>() { Text = customTableService.GetValueByCustomTableAndId(CustomTableType.AnimalGenre, attributeId), Value = attributeId };
                        break;

                    case ContentAttributeType.Age:
                        model.Months = attributeId;
                        break;

                    case ContentAttributeType.AutoReply:
                        model.AutoReply = Convert.ToBoolean(attribute.Value);
                        break;

                    case ContentAttributeType.Castrated:
                        model.Castrated = Convert.ToBoolean(attribute.Value);
                        break;

                    case ContentAttributeType.Size:
                        model.Size = new ContentAttributeModel<int>() { Text = customTableService.GetValueByCustomTableAndId(CustomTableType.AnimalSize, attributeId), Value = attributeId };
                        break;

                    case ContentAttributeType.Shelter:
                        var shelterContent = contentService.GetCachedShelter(cacheManager, attributeId);
                        model.Shelter = new ContentBaseModel() { Id = attributeId, Name = shelterContent != null ? shelterContent.Name : string.Empty };
                        break;

                    default:
                        break;
                }
            }

            return model;
        }

        /// <summary>
        /// To the pet models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="customTableService">The custom table service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <returns>the models</returns>
        public static IList<PetModel> ToPetModels(
            this IList<Content> entities, 
            IContentService contentService, 
            ICustomTableService customTableService, 
            ICacheManager cacheManager, 
            IFilesHelper filesHelper = null, 
            Func<string, string> contentUrlFunction = null, 
            bool withFiles = false,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0)
        {
            var models = new List<PetModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToPetModel(
                    contentService, 
                    customTableService, 
                    cacheManager, 
                    filesHelper, 
                    contentUrlFunction, 
                    withFiles,
                    false,
                    width,
                    height,
                    thumbnailWidth,
                    thumbnailHeight));
            }

            return models;
        }
    }
}