//-----------------------------------------------------------------------
// <copyright file="ContentAttributeExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Content Attribute Extensions
    /// </summary>
    public static class ContentAttributeExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static ContentAttributeModel ToModel(this ContentAttribute entity)
        {
            return new ContentAttributeModel()
            {
                Attribute = entity.AttributeType,
                Value = entity.Value
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<ContentAttributeModel> ToModels(this ICollection<ContentAttribute> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}