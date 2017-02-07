//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAttributeExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions.AdoptionForms
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.AdoptionForms;

    /// <summary>
    /// Adoption Form Attribute Extensions
    /// </summary>
    public static class AdoptionFormAttributeExtensions
    {
        /// <summary>
        /// To the entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the entities</returns>
        public static IList<AdoptionFormAttribute> ToEntities(this ICollection<AdoptionFormAttributeModel> entities)
        {
            return entities.Select(ToEntity).ToList();
        }

        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the entity</returns>
        public static AdoptionFormAttribute ToEntity(this AdoptionFormAttributeModel entity)
        {
            return new AdoptionFormAttribute
            {
                AttributeId = entity.AttributeId,
                Value = entity.Value
            };
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static AdoptionFormAttributeModel ToModel(this AdoptionFormAttribute entity)
        {
            return new AdoptionFormAttributeModel
            {
                AttributeId = entity.AttributeId,
                Question = entity.Attribute.Value,
                Value = entity.Value
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public static IList<AdoptionFormAttributeModel> ToModels(this ICollection<AdoptionFormAttribute> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}