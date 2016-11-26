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
    using Api.Users;
    using Business.Caching;
    using Business.Exceptions;
    using Business.Extensions.Services;
    using Business.Services.Common;
    using Business.Services.Contents;
    using Business.Services.Files;
    using Common;
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
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelState">State of the model.</param>
        /// <returns>
        ///   <c>true</c> if the specified model state is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this PetModel model, ModelStateDictionary modelState)
        {
            bool isValid = true;

            if (model.Files == null || model.Files.Count == 0)
            {
                modelState.AddModelError("Files", "Al menos se debe cargar una imagen");
                isValid = false;
            }

            if (model.Shelter == null && model.Location == null)
            {
                modelState.AddModelError("Location", "Si no ingresa la refugio debe ingresar ubicación");
                modelState.AddModelError("Shelter", "Si no ingresa la ubicación debe ingresar refugio");
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="contentService">The content service.</param>
        /// <returns>the value</returns>
        /// <exception cref="HuellitasException">the exceptions</exception>
        public static Content ToEntity(this PetModel model, IContentService contentService)
        {
            var entity = new Content();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Body = model.Body;
            entity.DisplayOrder = model.DisplayOrder;
            entity.Email = model.Email;
            entity.StatusType = StatusType.Created;
            entity.Type = ContentType.Pet;

            if (model.Shelter == null)
            {
                entity.LocationId = model.Location.Id;
            }
            else
            {
                var shelter = contentService.GetById(model.Shelter.Id);
                if (shelter != null)
                {
                    entity.LocationId = shelter.LocationId;
                    entity.ContentAttributes.Add(ContentAttributeType.Shelter, shelter.Id);
                }
                else
                {
                    throw new HuellitasException(HuellitasExceptionCode.ShelterNotFound);
                }
            }

            entity.ContentAttributes.Add(ContentAttributeType.AutoReply, model.AutoReply);
            entity.ContentAttributes.Add(ContentAttributeType.Age, model.Months);
            entity.ContentAttributes.Add(ContentAttributeType.Subtype, model.Subtype.Value);
            entity.ContentAttributes.Add(ContentAttributeType.Genre, model.Genre.Value);
            entity.ContentAttributes.Add(ContentAttributeType.Size, model.Size.Value);
            entity.ContentAttributes.Add(ContentAttributeType.Castrated, model.Castrated);

            return entity;
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="contentService">the content service</param>
        /// <param name="customTableService">the custom table service</param>
        /// <param name="cacheManager">the cache manager</param>
        /// <param name="filesHelper">the file helper</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if contains files or not in the response</param>
        /// <returns>the value</returns>
        public static PetModel ToPetModel(
            this Content entity, 
            IContentService contentService, 
            ICustomTableService customTableService, 
            ICacheManager cacheManager, 
            IFilesHelper filesHelper = null, 
            Func<string, string> contentUrlFunction = null, 
            bool withFiles = false, 
            bool withRelated = false)
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
                Featured = entity.Featured
            };

            if (entity.LocationId.HasValue)
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
                    .ToModels(filesHelper, contentUrlFunction);
            }

            if (withRelated)
            {
                model.RelatedPets = contentService.GetRelated(entity.Id, Data.Entities.Enums.RelationType.SimilarPets).ToPetModels(contentService, customTableService, cacheManager, filesHelper, contentUrlFunction, false);
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
                        model.Shelter = new ShelterModel() { Id = attributeId, Name = shelterContent != null ? shelterContent.Name : string.Empty };
                        break;

                    default:
                        break;
                }
            }

            return model;
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="contentService">The content service</param>
        /// <param name="customTableService">the custom table service</param>
        /// <param name="cacheManager">the cache manager</param>
        /// <param name="filesHelper">The file helper</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="withFiles">if contains files or not</param>
        /// <returns>the value</returns>
        public static IList<PetModel> ToPetModels(this IList<Content> entities, IContentService contentService, ICustomTableService customTableService, ICacheManager cacheManager, IFilesHelper filesHelper = null, Func<string, string> contentUrlFunction = null, bool withFiles = false)
        {
            var models = new List<PetModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToPetModel(contentService, customTableService, cacheManager, filesHelper, contentUrlFunction, withFiles));
            }

            return models;
        }
    }
}