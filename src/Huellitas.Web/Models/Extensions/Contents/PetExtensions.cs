﻿//-----------------------------------------------------------------------
// <copyright file="PetExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Api;
    using Beto.Core.Caching;
    using Beto.Core.Data.Files;
    using Business.Configuration;
    using Business.Exceptions;
    using Business.Extensions;
    using Business.Services;
    using Data.Entities;
    using Data.Extensions;
    using Huellitas.Business.Security;
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
        /// <param name="contentSettings">The content settings.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        /// <param name="entity">The entity.</param>
        /// <param name="files">The files.</param>
        /// <returns>the value</returns>
        /// <exception cref="HuellitasException">the exception</exception>
        public static Content ToEntity(
            this PetModel model,
            IContentSettings contentSettings,
            IContentService contentService,
            bool isAdmin,
            RoleEnum role,
            Content entity = null,
            IList<FileModel> files = null)
        {
            if (entity == null)
            {
                entity = new Content();
                entity.StatusType = StatusType.Created;
                entity.Type = model.Type;

                for (int i = 0; i < model.Files.Count; i++)
                {
                    if (i == 0)
                    {
                        entity.FileId = model.Files[i].Id;
                    }

                    entity.ContentFiles.Add(new ContentFile()
                    {
                        FileId = model.Files[i].Id,
                        DisplayOrder = model.Files.Count - i
                    });
                }
            }
            else
            {
                if (model.Files?.Count > 0)
                {
                    entity.FileId = model.Files.FirstOrDefault().Id;
                }
            }

            entity.Name = model.Name;
            entity.Body = model.Body;
            entity.DisplayOrder = model.DisplayOrder;
            entity.Email = model.Email;
            entity.ClosingDate = model.ClosingDate;
            entity.StartingDate = model.StartingDate;

            if (model.Shelter == null || model.Shelter.Id == 0)
            {
                entity.LocationId = model.Location.Id;
                entity.ContentAttributes.Remove(ContentAttributeType.Shelter);

                // Cuando no tiene shelter le da solo unos días de publicacion
                if (entity.Id == 0)
                {
                    if (role == RoleEnum.Public)
                    {
                        entity.ClosingDate = DateTime.UtcNow.AddDays(contentSettings.DaysToAutoClosingPet);
                    }
                    else if (role == RoleEnum.Rescuer)
                    {
                        entity.ClosingDate = DateTime.UtcNow.AddDays(contentSettings.DaysToAutoClosingPet * 12);
                    }                    
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

            if (isAdmin)
            {
                ////TODO:retest
                entity.Featured = model.Featured;
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
            entity.ContentAttributes.Add(ContentAttributeType.Breed, model.Breed?.Value, true);

            return entity;
        }

        /// <summary>
        /// To the pet model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="customTableService">The custom table service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
        /// <param name="withRelated">if set to <c>true</c> [with related].</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <param name="pendingForms">The pending forms.</param>
        /// <returns>the return</returns>
        public static PetModel ToPetModel(
            this Content entity,
            IContentService contentService,
            ICustomTableService customTableService,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IFilesHelper filesHelper = null,
            Func<string, string> contentUrlFunction = null,
            bool withFiles = false,
            bool withRelated = false,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0,
            int pendingForms = 0)
        {
            var model = new PetModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Body = entity.Body,
                CommentsCount = entity.CommentsCount,
                DisplayOrder = entity.DisplayOrder,
                Status = entity.StatusType,
                Type = entity.Type,
                Views = entity.Views,
                CreatedDate = entity.CreatedDate,
                Featured = entity.Featured,
                ClosingDate = entity.ClosingDate,
                StartingDate = entity.StartingDate,
                FriendlyName = entity.FriendlyName,
                CanEdit = workContext.CurrentUser.CanUserEditPet(entity, contentService),
                PendingForms = pendingForms
            };

            if (entity.LocationId.HasValue && entity.Location != null)
            {
                model.Location = new Api.LocationModel() { Id = entity.LocationId.Value, Name = entity.Location.Name };
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
                    .ToModels(filesHelper, contentUrlFunction, width, height, thumbnailWidth, thumbnailHeight);
            }

            if (withRelated)
            {
                model.RelatedPets = contentService.GetRelated(entity.Id, Data.Entities.RelationType.SimilarPets)
                    .ToPetModels(contentService, customTableService, cacheManager, workContext, filesHelper, contentUrlFunction, false);
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

                    case ContentAttributeType.Breed:
                        model.Breed = new ContentAttributeModel<int>() { Text = customTableService.GetValueByCustomTableAndId(CustomTableType.Breed, attributeId), Value = attributeId };
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
                        model.Shelter = shelterContent.ToShelterBaseModel(contentService, workContext, filesHelper, contentUrlFunction, width, height, 200, 200);
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
        /// <param name="workContext">The work context.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if set to <c>true</c> [with files].</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <param name="pendingForms">The pending forms.</param>
        /// <returns>the return</returns>
        public static IList<PetModel> ToPetModels(
            this IList<Content> entities,
            IContentService contentService,
            ICustomTableService customTableService,
            ICacheManager cacheManager,
            IWorkContext workContext,
            IFilesHelper filesHelper = null,
            Func<string, string> contentUrlFunction = null,
            bool withFiles = false,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0,
            IDictionary<int, int> pendingForms = null)
        {
            var models = new List<PetModel>();
            foreach (var entity in entities)
            {
                int pendingFormByContent = 0;
                if (pendingForms != null)
                {
                    pendingForms.TryGetValue(entity.Id, out pendingFormByContent);
                }

                models.Add(entity.ToPetModel(
                    contentService,
                    customTableService,
                    cacheManager,
                    workContext,
                    filesHelper,
                    contentUrlFunction,
                    withFiles,
                    false,
                    width,
                    height,
                    thumbnailWidth,
                    thumbnailHeight,
                    pendingFormByContent));
            }

            return models;
        }
    }
}