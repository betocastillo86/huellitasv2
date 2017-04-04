//-----------------------------------------------------------------------
// <copyright file="SystemSettingExtensions.cs" company="Huellitas sin hogar">
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
    /// System Setting Extensions
    /// </summary>
    public static class SystemSettingExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static SystemSettingModel ToModel(this SystemSetting entity)
        {
            return new SystemSettingModel
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
        public static IList<SystemSettingModel> ToModels(this ICollection<SystemSetting> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}