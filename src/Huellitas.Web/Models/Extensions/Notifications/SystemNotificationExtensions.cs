//-----------------------------------------------------------------------
// <copyright file="SystemNotificationExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Notifications;

    /// <summary>
    /// System Notification Extensions
    /// </summary>
    public static class SystemNotificationExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static SystemNotificationModel ToModel(this SystemNotification entity)
        {
            return new SystemNotificationModel()
            {
                Id = entity.Id,
                Seen = entity.Seen,
                TargetUrl = entity.TargetURL,
                Value = entity.Value
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<SystemNotificationModel> ToModels(this ICollection<SystemNotification> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}