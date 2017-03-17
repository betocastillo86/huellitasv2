using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huellitas.Data.Entities;
using Huellitas.Web.Models.Api.Notifications;

namespace Huellitas.Web.Models.Extensions.Notifications
{
    public static class NotificationExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static NotificationModel ToModel(this Notification entity)
        {
            return new Api.Notifications.NotificationModel
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
