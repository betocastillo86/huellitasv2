//-----------------------------------------------------------------------
// <copyright file="PetExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using Business.Exceptions;
    using Business.Services.Contents;
    using Data.Extensions;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Contents;

    /// <summary>
    /// Pet Extensions
    /// </summary>
    public static class PetExtensions
    {
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
                    shelter.LocationId = shelter.LocationId;
                    entity.ContentAttributes.Add(ContentAttributeType.Shelter, shelter.Id);
                }
                else
                {
                    throw new HuellitasException(HuellitasExceptionCode.ShelterNotFound);
                }
            }

            entity.ContentAttributes.Add(ContentAttributeType.AutoReply, model.AutoReply);
            entity.ContentAttributes.Add(ContentAttributeType.Age, model.Moths);
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
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <returns>the value</returns>
        public static PetModel ToModel(this Content entity, Func<string, string> contentUrlFunction = null)
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
                CreatedDate = entity.CreatedDate
            };

            if (entity.LocationId.HasValue)
            {
                model.Location = new Api.Common.LocationModel() { Id = entity.LocationId.Value, Name = entity.Location.Name };
            }

            foreach (var attribute in entity.ContentAttributes)
            {
                switch (attribute.AttributeType)
                {
                    case ContentAttributeType.Subtype:
                        model.Subtype = new ContentAttributeModel<int>() { Text = "s", Value = Convert.ToInt32(attribute.Value) };
                        break;

                    case ContentAttributeType.Genre:
                        model.Genre = new ContentAttributeModel<int>() { Text = "a", Value = Convert.ToInt32(attribute.Value) };
                        break;

                    case ContentAttributeType.Age:
                        model.Moths = Convert.ToInt32(attribute.Value);
                        break;

                    case ContentAttributeType.Size:
                        model.Size = new ContentAttributeModel<int>() { Text = "a", Value = Convert.ToInt32(attribute.Value) };
                        break;

                    case ContentAttributeType.Shelter:
                        model.Shelter = new ShelterModel() { Id = Convert.ToInt32(attribute.Value) };
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
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <returns>the value</returns>
        public static IList<PetModel> ToModels(this IList<Content> entities, Func<string, string> contentUrlFunction = null)
        {
            var models = new List<PetModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToModel(contentUrlFunction: contentUrlFunction));
            }

            return models;
        }
    }
}