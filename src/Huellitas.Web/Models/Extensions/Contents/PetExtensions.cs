//-----------------------------------------------------------------------
// <copyright file="PetExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Contents;
    
    /// <summary>
    /// Pet Extensions
    /// </summary>
    public static class PetExtensions
    {
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
                Views = entity.Views
            };

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
                        ////model.Shelter = new ContentAttributeModel<int>() { Text = "a", Value = Convert.ToInt32(attribute.Value) };
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