//-----------------------------------------------------------------------
// <copyright file="AdoptionFormNotifications.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Subscribers.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Notifications;
    using Configuration;
    using Data.Entities.Enums;
    using Data.Extensions;
    using Exceptions;
    using Extensions.Entities;
    using Extensions.Services;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Data.Entities;
    using Security;
    using Services.AdoptionForms;
    using Services.Common;
    using Services.Contents;
    using Services.Notifications;
    using Services.Seo;
    using Services.Users;

    /// <summary>
    /// Adoption Form Notifications
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.AdoptionForm}}" />
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.AdoptionFormAnswer}}" />
    public class AdoptionFormNotifications : ISubscriber<EntityInsertedMessage<AdoptionForm>>,
        ISubscriber<EntityInsertedMessage<AdoptionFormAnswer>>,
        ISubscriber<EntityInsertedMessage<AdoptionFormUser>>
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The location service
        /// </summary>
        private readonly ILocationService locationService;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The SEO service
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
        /// Initializes a new instance of the <see cref="AdoptionFormNotifications"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="seoService">The SEO service.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="adoptionFormService">The adoption form service.</param>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="locationService">The location service.</param>
        public AdoptionFormNotifications(
            INotificationService notificationService,
            IWorkContext workContext,
            ISeoService seoService,
            IContentService contentService,
            IUserService userService,
            IAdoptionFormService adoptionFormService,
            IGeneralSettings generalSettings,
            ILocationService locationService)
        {
            this.notificationService = notificationService;
            this.workContext = workContext;
            this.seoService = seoService;
            this.contentService = contentService;
            this.userService = userService;
            this.adoptionFormService = adoptionFormService;
            this.generalSettings = generalSettings;
            this.locationService = locationService;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<AdoptionForm> message)
        {
            var form = message.Entity;
            var content = this.GetContentByForm(form);
            var shelter = this.contentService.GetShelterByPet(content.Id);

            await this.NotifyUserOfFormCreated(form, content, shelter);

            await this.NotifyPetOwnersOfFormCreated(form, content, shelter);
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<AdoptionFormUser> message)
        {
            var userForm = message.Entity;
            var form = this.adoptionFormService.GetById(userForm.AdoptionFormId);
            var content = this.GetContentByForm(form);
            var shelter = this.contentService.GetShelterByPet(content.Id);

            await this.NotifyUserOfFormShared(form, content, shelter);
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<AdoptionFormAnswer> message)
        {
            var answer = message.Entity;
            var form = this.adoptionFormService.GetById(answer.AdoptionFormId);
            var content = this.GetContentByForm(form);
            var shelter = this.contentService.GetShelterByPet(content.Id);
            var user = form.User;

            await this.NotifyAdoptionFormAnswer(answer, user, content, shelter);
        }

        /// <summary>
        /// Gets the basic parameters.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="shelter">The shelter.</param>
        /// <returns>the parameters</returns>
        private IList<NotificationParameter> GetBasicParameters(Content content, Content shelter)
        {
            var petUrl = this.seoService.GetContentUrl(content);
            var parameters = new List<NotificationParameter>();
            parameters.Add("Pet.Name", content.Name);
            parameters.Add("Pet.Url", petUrl);

            if (shelter != null)
            {
                parameters.Add("Shelter.Name", shelter.Name);
                parameters.Add("Shelter.Url", this.seoService.GetContentUrl(shelter));
                parameters.Add("Shelter.Phone", shelter.GetAttribute<string>(ContentAttributeType.Phone1));
                parameters.Add("Shelter.Address", shelter.GetShelterAddress(this.locationService));
                parameters.Add("Shelter.Email", shelter.Email ?? "No disponible");
            }
            else
            {
                parameters.Add("Shelter.Name", content.User.Name);
                parameters.Add("Shelter.Url", string.Empty);
                parameters.Add("Shelter.Phone", content.User.PhoneNumber);
                parameters.Add("Shelter.Address", "No disponible");
                parameters.Add("Shelter.Email", content.User.Email);
            }

            return parameters;
        }

        /// <summary>
        /// Gets the content by form.
        /// </summary>
        /// <param name="adoptionForm">The adoption form.</param>
        /// <returns>the content</returns>
        private Content GetContentByForm(AdoptionForm adoptionForm)
        {
            if (adoptionForm.Content != null)
            {
                return adoptionForm.Content;
            }
            else
            {
                return this.contentService.GetById(adoptionForm.ContentId);
            }
        }

        /// <summary>
        /// Notifies the adoption form answer.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="user">The user.</param>
        /// <param name="content">The content.</param>
        /// <param name="shelter">The shelter.</param>
        /// <returns>the task</returns>
        private async Task NotifyAdoptionFormAnswer(AdoptionFormAnswer answer, User user, Content content, Content shelter)
        {
            var parameters = this.GetBasicParameters(content, shelter);
            parameters.Add("Answer.AdditionalInfo", answer.AdditionalInfo);

            var template = NotificationType.AdoptionFormApproved;
            string url = null;

            switch (answer.StatusEnum)
            {
                case AdoptionFormAnswerStatus.Approved:
                    template = NotificationType.AdoptionFormApproved;
                    url = shelter != null ? this.seoService.GetContentUrl(shelter) : string.Empty;
                    break;

                case AdoptionFormAnswerStatus.Denied:
                    template = NotificationType.AdoptionFormRejected;
                    url = this.generalSettings.SiteUrl;
                    break;

                case AdoptionFormAnswerStatus.AlreadyAdopted:
                    template = NotificationType.AdoptionFormAlreadyAdopted;
                    url = this.generalSettings.SiteUrl;
                    break;

                default:
                    throw new HuellitasException("Respuesta invalida");
            }

            await this.notificationService.NewNotification(
                user,
                null,
                template,
                url,
                parameters);
        }

        /// <summary>
        /// Notifies the pet owners of form created.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="content">The content.</param>
        /// <param name="shelter">The shelter.</param>
        /// <returns>the task</returns>
        private async Task NotifyPetOwnersOfFormCreated(AdoptionForm form, Content content, Content shelter)
        {
            var parameters = this.GetBasicParameters(content, shelter);

            List<User> users = null;

            if (shelter == null)
            {
                users.Add(content.User);
            }
            else
            {
                users = this.contentService.GetUsersByContentId(shelter.Id, Data.Entities.Enums.ContentUserRelationType.Shelter, true)
                    .Select(c => c.User)
                    .ToList();
            }

            var parents = this.contentService.GetUsersByContentId(content.Id, ContentUserRelationType.Parent, true)
                .Select(c => c.User)
                .ToList();

            users.AddRange(parents);

            await this.notificationService.NewNotification(
                users,
                this.workContext.CurrentUser,
                Data.Entities.Enums.NotificationType.AdoptionFormReceived,
                ////TODO:URL para responder formulario
                this.seoService.GetContentUrl(content),
                parameters);
        }

        /// <summary>
        /// Notifies the user of form created.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="content">The content.</param>
        /// <param name="shelter">The shelter.</param>
        /// <returns>the task</returns>
        private async Task NotifyUserOfFormCreated(AdoptionForm form, Content content, Content shelter)
        {
            var parameters = this.GetBasicParameters(content, shelter);

            await this.notificationService.NewNotification(
                this.workContext.CurrentUser,
                null,
                Data.Entities.Enums.NotificationType.AdoptionFormConfirmation,
                this.seoService.GetContentUrl(content),
                parameters);
        }

        /// <summary>
        /// Notifies the user of form shared.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="content">The content.</param>
        /// <param name="shelter">The shelter.</param>
        /// <returns>the task</returns>
        private async Task NotifyUserOfFormShared(AdoptionForm form, Content content, Content shelter)
        {
            var parameters = this.GetBasicParameters(content, shelter);

            await this.notificationService.NewNotification(
                form.User,
                null,
                Data.Entities.Enums.NotificationType.AdoptionFormShared,
                this.seoService.GetContentUrl(content),
                parameters);
        }
    }
}