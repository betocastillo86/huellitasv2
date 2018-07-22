//-----------------------------------------------------------------------
// <copyright file="UserNotifications.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Subscribers.Notifications
{
    using Huellitas.Business.Configuration;
    using Beto.Core.EventPublisher;
    using Huellitas.Business.Notifications;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// User Notifications
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.User}}" />
    public class UserNotifications : ISubscriber<EntityInsertedMessage<User>>
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotifications"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        public UserNotifications(INotificationService notificationService,
            IGeneralSettings generalSettings)
        {
            this.notificationService = notificationService;
            this.generalSettings = generalSettings;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<User> message)
        {
            // Notifies the user register
            await this.notificationService.NewNotification(
                message.Entity,
                null,
                Data.Entities.NotificationType.SignUp,
                this.generalSettings.SiteUrl,
                new List<NotificationParameter>());
        }
    }
}