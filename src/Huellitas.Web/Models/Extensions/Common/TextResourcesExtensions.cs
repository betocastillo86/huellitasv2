//-----------------------------------------------------------------------
// <copyright file="TextResourcesExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Text Resources Extensions
    /// </summary>
    public static class TextResourcesExtensions
    {
        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the entity</returns>
        public static TextResource ToEntity(this TextResourceModel model)
        {
            return new TextResource
            {
                Id = model.Id,
                Name = model.Name,
                Value = model.Value
            };
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static TextResourceModel ToModel(this TextResource entity)
        {
            return new TextResourceModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<TextResourceModel> ToModels(this ICollection<TextResource> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}