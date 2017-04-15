//-----------------------------------------------------------------------
// <copyright file="RoleMappingExtensions.cs" company="Huellitas sin hogar">
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
    /// Role Mapping Extensions
    /// </summary>
    public static class RoleMappingExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static RoleModel ToModel(this Role entity)
        {
            return new RoleModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the list</returns>
        public static IList<RoleModel> ToModels(this IEnumerable<Role> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}