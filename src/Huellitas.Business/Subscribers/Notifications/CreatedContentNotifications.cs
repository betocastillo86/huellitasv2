namespace Huellitas.Business.Subscribers
{
    using Huellitas.Business.EventPublisher;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Notifications;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Notification of the process of creating a pet
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.Content}}" />
    public class CreatedContentNotifications : 
        ISubscriber<EntityInsertedMessage<Content>>,
        ISubscriber<ContentAprovedModel>
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The seo service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedContentNotifications"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="routingService">The routing service.</param>
        /// <param name="seoService">The seo service.</param>
        /// <param name="userService">The user service.</param>
        public CreatedContentNotifications(
            INotificationService notificationService,
            IWorkContext workContext,
            ISeoService seoService,
            IUserService userService)
        {
            this.notificationService = notificationService;
            this.workContext = workContext;
            this.seoService = seoService;
            this.userService = userService;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<Content> message)
        {
            if (message.Entity.Type == ContentType.LostPet || message.Entity.Type == ContentType.Pet)
            {
                await this.NotifyCreatedPetConfirmation(message.Entity);
            }
            else
            {
                await this.NotifyCreatedShelterConfirmation(message.Entity);
                await this.NotifyCreatedShelterToAdmins(message.Entity);
            }
        }

        private async Task NotifyCreatedShelterToAdmins(Content content)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                var shelterUrl = this.seoService.GetContentUrl(content);
                var parameters = new List<NotificationParameter>();
                parameters.Add("Shelter.Name", content.Name);
                parameters.Add("Shelter.Url", shelterUrl);

                var administrators = await this.userService.GetAll(role: RoleEnum.SuperAdmin);

                await this.notificationService.NewNotification(
                    administrators,
                    null,
                    NotificationType.NewShelterRequest,
                    shelterUrl,
                    parameters);
            }
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(ContentAprovedModel message)
        {
            if (message.Content.Type == ContentType.Pet)
            {
                await this.NotifyPetAproved(message.Content);
            }
            else if (message.Content.Type == ContentType.Shelter)
            {
                await this.NotifyShelterAprovedOrRejected(message.Content);
            }
        }

        /// <summary>
        /// Notifies the created pet confirmation.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        private async Task NotifyCreatedPetConfirmation(Content content)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                var petUrl = this.seoService.GetContentUrl(content);
                var parameters = new List<NotificationParameter>();
                parameters.Add("Pet.Name", content.Name);
                parameters.Add("Pet.Url", petUrl);

                await this.notificationService.NewNotification(
                    this.workContext.CurrentUser,
                    null,
                    content.Type == ContentType.Pet ? NotificationType.CreatedPetConfirmation : NotificationType.CreatedLostPetConfirmation,
                    petUrl,
                    parameters);
            }
        }

        /// <summary>
        /// Notifies the created shelter confirmation.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        private async Task NotifyCreatedShelterConfirmation(Content content)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                var shelterUrl = this.seoService.GetContentUrl(content);
                var parameters = new List<NotificationParameter>();
                parameters.Add("Shelter.Name", content.Name);
                parameters.Add("Shelter.Url", shelterUrl);

                await this.notificationService.NewNotification(
                    this.workContext.CurrentUser,
                    null,
                    NotificationType.ShelterRequestReceived,
                    shelterUrl,
                    parameters);
            }
        }



        /// <summary>
        /// Notifies the pet aproved.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        private async Task NotifyPetAproved(Content content)
        {
            var user = this.userService.GetById(content.UserId);

            if (!user.IsSuperAdmin())
            {
                var petUrl = this.seoService.GetContentUrl(content);
                var parameters = new List<NotificationParameter>();
                parameters.Add("Pet.Name", content.Name);
                parameters.Add("Pet.Url", petUrl);

                await this.notificationService.NewNotification(
                    user,
                    null,
                    NotificationType.PetApproved,
                    petUrl,
                    parameters);
            }
        }

        private async Task NotifyShelterAprovedOrRejected(Content content)
        {
            var user = this.userService.GetById(content.UserId);

            if (!user.IsSuperAdmin())
            {
                var shelterUrl = this.seoService.GetContentUrl(content);
                var parameters = new List<NotificationParameter>();
                parameters.Add("Shelter.Name", content.Name);
                parameters.Add("Shelter.Url", shelterUrl);

                await this.notificationService.NewNotification(
                    user,
                    null,
                    content.StatusType == StatusType.Published ? NotificationType.ShelterApproved : NotificationType.ShelterRequestRejected,
                    shelterUrl,
                    parameters);
            }
        }
    }
}