//-----------------------------------------------------------------------
// <copyright file="LocationExtensions.cs" company="Huellitas sin hogar">
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
    /// Location Extensions
    /// </summary>
    public static class LocationExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static LocationModel ToModel(this Location entity)
        {
            return new LocationModel { Id = entity.Id, Name = entity.Name };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the list</returns>
        public static IList<LocationModel> ToModels(this IEnumerable<Location> entities)
        {
            return entities.Select(LocationExtensions.ToModel).ToList();
        }
    }
}