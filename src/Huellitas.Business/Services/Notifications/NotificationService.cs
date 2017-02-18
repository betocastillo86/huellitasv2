﻿//-----------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Business.Configuration;
    using Caching;
    using Data.Core;
    using EventPublisher;
    using Exceptions;
    using Huellitas.Business.Notifications;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Data.Infraestructure;
    using Users;

    /// <summary>
    /// Notification Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Notifications.INotificationService" />
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The email notification repository
        /// </summary>
        private readonly IRepository<EmailNotification> emailNotificationRepository;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The notification repository
        /// </summary>
        private readonly IRepository<Notification> notificationRepository;

        /// <summary>
        /// The notification settings
        /// </summary>
        private readonly INotificationSettings notificationSettings;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// The system notification repository
        /// </summary>
        private readonly IRepository<SystemNotification> systemNotificationRepository;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="notificationSettings">The notification settings.</param>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="systemNotificationRepository">The system notification repository.</param>
        /// <param name="emailNotificationRepository">The email notification repository.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="publisher">The publisher.</param>
        public NotificationService(
            IUserService userService,
            GeneralSettings generalSettings,
            NotificationSettings notificationSettings,
            IRepository<Notification> notificationRepository,
            IRepository<SystemNotification> systemNotificationRepository,
            IRepository<EmailNotification> emailNotificationRepository,
            ICacheManager cacheManager,
            IPublisher publisher)
        {
            this.userService = userService;
            this.generalSettings = generalSettings;
            this.notificationSettings = notificationSettings;
            this.notificationRepository = notificationRepository;
            this.systemNotificationRepository = systemNotificationRepository;
            this.emailNotificationRepository = emailNotificationRepository;
            this.cacheManager = cacheManager;
            this.publisher = publisher;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// the value
        /// </returns>
        public IList<Notification> GetAll()
        {
            return this.notificationRepository.Table.ToList();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the notification
        /// </returns>
        public Notification GetById(int id)
        {
            return this.notificationRepository.GetById(id);
        }

        /// <summary>
        /// Gets the cached notification.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// the value
        /// </returns>
        public Notification GetCachedNotification(NotificationType type)
        {
            var notificationId = Convert.ToInt32(type);
            return this.GetCachedNotifications()
                .FirstOrDefault(n => n.Id == notificationId);
        }

        /// <summary>
        /// Gets the cached notifications.
        /// </summary>
        /// <returns>
        /// the value
        /// </returns>
        public IList<Notification> GetCachedNotifications()
        {
            return this.cacheManager.Get(
                CacheKeys.NOTIFICATIONS_ALL,
                () =>
                {
                    return this.notificationRepository.Table.ToList();
                });
        }

        /// <summary>
        /// Gets the user notifications.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="size">The size.</param>
        /// <returns>
        /// the value
        /// </returns>
        public IPagedList<SystemNotification> GetUserNotifications(int userId, int page, int size)
        {
            var query = this.systemNotificationRepository.Table
                .Where(sn => sn.UserId.Equals(userId))
                .OrderByDescending(sn => sn.CreationDate);

            return new PagedList<SystemNotification>(query, page, size);
        }

        /// <summary>
        /// Inserts the notification.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the task</returns>
        public async Task NewNotification(IList<User> users, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters)
        {
            if (type == NotificationType.Manual)
            {
                throw new HuellitasException("Faltan los parametros 'de', asunto, y contenido");
            }

            await this.NewNotification(users, userTriggerEvent, type, targetUrl, parameters, null, null, null);
        }

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification(User user, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters)
        {
            if (type == NotificationType.Manual)
            {
                throw new HuellitasException("Faltan los parametros 'de', asunto, y contenido");
            }

            await this.NewNotification(user, userTriggerEvent, type, targetUrl, parameters, null, null, null);
        }

        /// <summary>
        /// Inserts the specified notification.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="defaultFromName">The default from name.</param>
        /// <param name="defaultSubject">The default subject.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification(User user, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters, string defaultFromName, string defaultSubject, string defaultMessage)
        {
            var list = new List<User>() { user };
            await this.NewNotification(list, userTriggerEvent, type, targetUrl, parameters, defaultFromName, defaultSubject, defaultMessage);
        }

        /// <summary>
        /// Inserts the specified notifications.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="defaultFromName">The default from name.</param>
        /// <param name="defaultSubject">The default subject.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification(IList<User> users, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters, string defaultFromName, string defaultSubject, string defaultMessage)
        {
            ////En los casos manuales no las busca, sino que quedan quemadas
            var notification = type != NotificationType.Manual ? this.GetCachedNotification(type) : new Notification() { Active = true, IsEmail = true };

            if (parameters == null)
            {
                parameters = new List<NotificationParameter>();
            }

            if (notification == null)
            {
                throw new HuellitasException(HuellitasExceptionCode.RowNotFound);
            }

            if (notification.Active)
            {
                ////Listado de usuarios a los que no se les envía la notificación
                var usersNotSend = new List<int>();

                ////Se agrega la raiz del sitio
                parameters.Add("RootUrl", this.generalSettings.SiteUrl);

                ////Asigna por defecto el parametro url el target url
                if (!string.IsNullOrEmpty(targetUrl) && !parameters.Any(c => c.Key.Equals("Url")))
                {
                    parameters.Add("Url", targetUrl);
                }

                parameters.Add("FacebookUrl", this.generalSettings.FacebookUrl);
                parameters.Add("InstagramUrl", this.generalSettings.InstagramUrl);

                var systemNotificationsToInsert = new List<SystemNotification>();
                var emailNotificationsToInsert = new List<EmailNotification>();

                ////Recorre los usuarios a los que debe realizar la notificación
                foreach (var user in users)
                {
                    ////Si la notificación es del sistema la envia
                    if (notification.IsSystem)
                    {
                        var systemNotification = new SystemNotification();
                        systemNotification.UserId = user.Id;
                        systemNotification.Value = this.GetStringFormatted(notification.SystemText, parameters);
                        systemNotification.TargetURL = targetUrl;
                        systemNotification.CreationDate = DateTime.Now;
                        systemNotification.Seen = false;

                        if (userTriggerEvent != null)
                        {
                            systemNotification.TriggerUserId = userTriggerEvent.Id;
                        }

                        ////Inserta la notificación de este tipo
                        systemNotificationsToInsert.Add(systemNotification);
                    }

                    if (notification.IsEmail && !string.IsNullOrWhiteSpace(user.Email))
                    {
                        var emailNotification = this.GetEmailNotificationToAdd(notification, type, user, parameters, defaultFromName, defaultSubject, defaultMessage);
                        emailNotificationsToInsert.Add(emailNotification);
                    }
                }

                if (emailNotificationsToInsert.Count > 0)
                {
                    await this.emailNotificationRepository.InsertAsync(emailNotificationsToInsert);
                }

                if (systemNotificationsToInsert.Count > 0)
                {
                    await this.systemNotificationRepository.InsertAsync(systemNotificationsToInsert);
                }
            }
        }

        /// <summary>
        /// News the notification email.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="notificationType">Type of the notification.</param>
        /// <param name="to">To message</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotificationEmail(
            IList<NotificationParameter> parameters,
            NotificationType notificationType,
            string to)
        {
            DateTime now = DateTime.Now;

            if (parameters == null)
            {
                parameters = new List<NotificationParameter>();
            }

            parameters.Add("RootUrl", this.generalSettings.SiteUrl);

            var notification = this.GetCachedNotification(notificationType);

            string message = this.GetStringFormatted(notification.EmailHtml, parameters);
            string body = this.notificationSettings.BodyBaseHtml.Replace("%%Body%%", message);

            await this.emailNotificationRepository.InsertAsync(new EmailNotification()
            {
                To = to,
                ToName = to,
                Subject = notification.EmailSubject,
                Body = body,
                CreatedDate = DateTime.Now,
                Cc = null,
                ScheduledDate = null,
                SentDate = null
            });
        }

        /// <summary>
        /// Get the seen notifications.
        /// </summary>
        /// <param name="notifications">The notifications.</param>
        /// <returns>
        /// the value
        /// </returns>
        public async Task SeenNotifications(int[] notifications)
        {
            var toUpdate = this.systemNotificationRepository.Table
                .Where(s => notifications.Contains(s.Id))
                .ToList();

            foreach (var notification in toUpdate)
            {
                notification.Seen = true;
            }

            await this.systemNotificationRepository.UpdateAsync(toUpdate);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update(Notification entity)
        {
            entity.UpdateDate = DateTime.Now;

            await this.notificationRepository.UpdateAsync(entity);

            this.publisher.EntityUpdated(entity);
        }

        /// <summary>
        /// Gets the email notification to add.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="type">The type.</param>
        /// <param name="user">The user.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="defaultFromName">The default from name.</param>
        /// <param name="defaultSubject">The default subject.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <returns>the notification</returns>
        /// <exception cref="System.ArgumentNullException">when fromName is null</exception>
        private EmailNotification GetEmailNotificationToAdd(
            Notification notification,
            NotificationType type,
            User user,
            IList<NotificationParameter> parameters,
            string defaultFromName,
            string defaultSubject,
            string defaultMessage)
        {
            ////TODO:Test
            string fromName = string.Empty;
            string subject = string.Empty;
            string message = string.Empty;

            ////Cuando es manual envia otros parametros
            if (type == NotificationType.Manual)
            {
                if (string.IsNullOrEmpty(defaultFromName) || string.IsNullOrEmpty(defaultSubject) || string.IsNullOrEmpty(defaultMessage))
                {
                    throw new ArgumentNullException("fromName");
                }

                fromName = defaultFromName;
                subject = defaultSubject;
                message = defaultMessage;
            }
            else
            {
                fromName = this.notificationSettings.EmailSenderName;
                subject = this.GetStringFormatted(notification.EmailSubject, parameters);
                message = this.GetStringFormatted(notification.EmailHtml, parameters);
            }

            ////Reemplaza el HTML
            string body = this.notificationSettings.BodyBaseHtml.Replace("%%Body%%", message);

            return new EmailNotification()
            {
                To = user.Email,
                ToName = user.Name,
                Subject = subject,
                Body = body,
                CreatedDate = DateTime.Now,
                Cc = null,
                ScheduledDate = null,
                SentDate = null
            };
        }

        /// <summary>
        /// Gets the string formatted.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the value</returns>
        private string GetStringFormatted(string value, IList<NotificationParameter> parameters)
        {
            if (parameters == null)
            {
                return value;
            }

            var strValue = new StringBuilder(value);
            foreach (var parameter in parameters)
            {
                strValue.Replace(parameter.Key, parameter.Value);
            }

            return strValue.ToString();
        }
    }
}