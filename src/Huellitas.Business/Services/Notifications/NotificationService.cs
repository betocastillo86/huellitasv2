//-----------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Beto.Core.Data.Notifications;
    using Beto.Core.Data.Users;
    using Beto.Core.EventPublisher;
    using Business.Configuration;
    using Caching;
    using Exceptions;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Notification Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.INotificationService" />
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
        /// The core notification service
        /// </summary>
        private readonly ICoreNotificationService coreNotificationService;

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
            IGeneralSettings generalSettings,
            INotificationSettings notificationSettings,
            IRepository<Notification> notificationRepository,
            IRepository<SystemNotification> systemNotificationRepository,
            IRepository<EmailNotification> emailNotificationRepository,
            ICacheManager cacheManager,
            IPublisher publisher,
            ICoreNotificationService coreNotificationService)
        {
            this.userService = userService;
            this.generalSettings = generalSettings;
            this.notificationSettings = notificationSettings;
            this.notificationRepository = notificationRepository;
            this.systemNotificationRepository = systemNotificationRepository;
            this.emailNotificationRepository = emailNotificationRepository;
            this.cacheManager = cacheManager;
            this.publisher = publisher;
            this.coreNotificationService = coreNotificationService;
        }

        /// <summary>
        /// Counts the unseen notifications by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>the number of unseen notifications</returns>
        public int CountUnseenNotificationsByUserId(int userId)
        {
            return this.systemNotificationRepository.Table.Count(c => c.UserId == userId && !c.Seen);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the notifications
        /// </returns>
        public IPagedList<Notification> GetAll(string name = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.notificationRepository.Table;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            return new PagedList<Notification>(query, page, pageSize);
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
            return this.notificationRepository.Table.FirstOrDefault(c => c.Id == id);
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
        /// Gets the email notifications.
        /// </summary>
        /// <param name="sent">The sent.</param>
        /// <param name="to">the To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the notifications
        /// </returns>
        public IPagedList<EmailNotification> GetEmailNotifications(
            bool? sent = default(bool?),
            string to = null,
            string subject = null,
            string body = null,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.emailNotificationRepository.Table;

            if (sent.HasValue)
            {
                query = sent.Value ? query.Where(c => c.SentDate != null) : query.Where(c => c.SentDate == null);
            }

            if (!string.IsNullOrEmpty(to))
            {
                query = query.Where(c => c.To.Contains(to));
            }

            if (!string.IsNullOrEmpty(subject))
            {
                query = query.Where(c => c.Subject.Contains(subject));
            }

            if (!string.IsNullOrEmpty(body))
            {
                query = query.Where(c => c.Body.Contains(body));
            }

            query = query.OrderByDescending(c => c.CreatedDate);

            return new PagedList<EmailNotification>(query, page, pageSize);
        }

        /// <summary>
        /// Gets the user notifications.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">The size.</param>
        /// <returns>
        /// the value
        /// </returns>
        public IPagedList<SystemNotification> GetUserNotifications(int userId, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.systemNotificationRepository.Table
                .Where(sn => sn.UserId.Equals(userId))
                .OrderByDescending(sn => sn.CreationDate);

            return new PagedList<SystemNotification>(query, page, pageSize);
        }

        /// <summary>
        /// Inserts the email notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task InsertEmailNotification(EmailNotification notification)
        {
            notification.CreatedDate = DateTime.Now;
            await this.emailNotificationRepository.InsertAsync(notification);
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
            var notificationId = Convert.ToInt32(type);
            var notification = this.GetCachedNotifications()
                .FirstOrDefault(n => n.Id == notificationId);

            var settings = new Beto.Core.Data.Notifications.NotificationSettings()
            {
                BaseHtml = this.notificationSettings.BodyBaseHtml,
                DefaultFromName = defaultFromName,
                DefaultMessage = defaultMessage,
                DefaultSubject = defaultSubject,
                IsManual = false,
                SiteUrl = this.generalSettings.SiteUrl
            };

            await this.coreNotificationService.NewNotification<SystemNotification, EmailNotification>(
                users.Select(c => (IUserEntity)c).ToList(),
                userTriggerEvent,
                notification,
                targetUrl,
                parameters,
                settings);
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
            string body = this.notificationSettings.BodyBaseHtml
                .Replace("%%Body%%", message)
                .Replace("%%RootUrl%%", this.generalSettings.SiteUrl);

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

            await this.publisher.EntityUpdated(entity);
        }

        /// <summary>
        /// Updates the email notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task UpdateEmailNotification(EmailNotification notification)
        {
            await this.emailNotificationRepository.UpdateAsync(notification);
        }

        /// <summary>
        /// Gets the email notification by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the notification
        /// </returns>
        public EmailNotification GetEmailNotificationById(int id)
        {
            return this.emailNotificationRepository.Table.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Updates the notifications to seen.
        /// </summary>
        /// <param name="notifications">The notifications.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task UpdateNotificationsToSeen(int[] notifications)
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
            string body = this.notificationSettings.BodyBaseHtml
                .Replace("%%Body%%", message)
                .Replace("%%RootUrl%%", this.generalSettings.SiteUrl);

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