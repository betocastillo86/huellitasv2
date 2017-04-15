//-----------------------------------------------------------------------
// <copyright file="EmailNotificationExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Api;
    using Data.Entities;

    /// <summary>
    /// Email Notification Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseModel" />
    public static class EmailNotificationExtensions
    {
        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the entity</returns>
        public static EmailNotification ToEntity(this EmailNotificationModel model)
        {
            return new EmailNotification
            {
                Id = model.Id,
                CreatedDate = model.CreatedDate,
                SentDate = model.SentDate,
                SentTries = model.SentTries,
                Subject = model.Subject,
                To = model.To,
                Body = model.Body
            };
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static EmailNotificationModel ToModel(this EmailNotification entity)
        {
            return new EmailNotificationModel
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                SentDate = entity.SentDate,
                SentTries = entity.SentTries,
                Subject = entity.Subject,
                To = entity.To,
                Body = entity.Body
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<EmailNotificationModel> ToModels(this ICollection<EmailNotification> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}