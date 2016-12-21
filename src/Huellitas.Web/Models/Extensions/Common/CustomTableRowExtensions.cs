//-----------------------------------------------------------------------
// <copyright file="CustomTableRowExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Common;

    /// <summary>
    /// Custom Table row Extensions
    /// </summary>
    public static class CustomTableRowExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static CustomTableRowModel ToModel(this CustomTableRow entity)
        {
            ////TODO:Test
            return new CustomTableRowModel
            {
                Id = entity.Id,
                Value = entity.Value
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<CustomTableRowModel> ToModels(this ICollection<CustomTableRow> entities)
        {
            return entities.Select(c => ToModel(c)).ToList();
        }
    }
}