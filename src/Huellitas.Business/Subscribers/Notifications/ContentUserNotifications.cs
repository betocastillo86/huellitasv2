//-----------------------------------------------------------------------
// <copyright file="ContentUserNotifications.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Subscribers.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Business.Extensions.Entities;
    using Huellitas.Business.Notifications;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services.Common;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Notifications;
    using Huellitas.Business.Services.Seo;
    using Huellitas.Business.Services.Users;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Data.Extensions;

    /// <summary>
    /// Content User Notifications
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.ContentUser}}" />
    public class ContentUserNotifications : ISubscriber<EntityInsertedMessage<ContentUser>>
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The location service
        /// </summary>
        private readonly ILocationService locationService;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The seo service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentUserNotifications"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="locationService">The location service.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="seoService">The SEO service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="workContext">The work context.</param>
        public ContentUserNotifications(
            IContentService contentService,
            ILocationService locationService,
            INotificationService notificationService,
            ISeoService seoService,
            IUserService userService,
            IWorkContext workContext)
        {
            this.contentService = contentService;
            this.locationService = locationService;
            this.notificationService = notificationService;
            this.seoService = seoService;
            this.userService = userService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<ContentUser> message)
        {
            var contentUser = message.Entity;

            var content = contentUser.Content ?? this.contentService.GetById(contentUser.ContentId);
            var user = contentUser.User ?? await this.userService.GetById(contentUser.UserId);

            switch (contentUser.RelationType)
            {
                case ContentUserRelationType.Shelter:
                    await this.NotifyUserAddedToShelter(content, user);
                    break;

                case ContentUserRelationType.Parent:
                    await this.NotifyParentAdded(content, user);
                    break;
            }
        }

        /// <summary>
        /// Notifies the parent added.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        public async Task NotifyParentAdded(Content content, User user)
        {
            var shelterUrl = this.seoService.GetContentUrl(content);

            var petUrl = this.seoService.GetContentUrl(content);
            var parameters = new List<NotificationParameter>();
            parameters.Add("Pet.Name", content.Name);
            parameters.Add("Pet.Url", petUrl);

            await this.notificationService.NewNotification(
                user,
                this.workContext.CurrentUser,
                NotificationType.ParentAddedToPet,
                this.seoService.GetContentUrl(content),
                parameters);
        }

        /// <summary>
        /// Notifies the user added to shelter.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        public async Task NotifyUserAddedToShelter(Content content, User user)
        {
            var shelterUrl = this.seoService.GetContentUrl(content);

            var parameters = new List<NotificationParameter>();
            parameters.Add("Shelter.Name", content.Name);
            parameters.Add("Shelter.Url", shelterUrl);
            parameters.Add("Shelter.Phone", content.GetAttribute<string>(ContentAttributeType.Phone1));
            parameters.Add("Shelter.Address", content.GetShelterAddress(this.locationService));
            parameters.Add("Shelter.Email", content.Email ?? "No disponible");

            await this.notificationService.NewNotification(
                user,
                this.workContext.CurrentUser,
                NotificationType.UserAddedToShelter,
                this.seoService.GetContentUrl(content),
                parameters);
        }
    }
}