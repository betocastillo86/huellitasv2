//-----------------------------------------------------------------------
// <copyright file="NotificationExtensions.cs" company="Huellitas sin hogar">
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
    /// Notification Extensions
    /// </summary>
    public static class NotificationExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static NotificationModel ToModel(this Notification entity)
        {
            return new Api.NotificationModel
            {
                Active = entity.Active,
                EmailHtml = entity.EmailHtml,
                EmailSubject = entity.EmailSubject,
                IsEmail = entity.IsEmail,
                IsSystem = entity.IsSystem,
                Name = entity.Name,
                SystemText = entity.SystemText,
                Id = entity.Id,
                Tags = entity.Tags
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<NotificationModel> ToModels(this ICollection<Notification> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}