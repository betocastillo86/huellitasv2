﻿//-----------------------------------------------------------------------
// <copyright file="INotificationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Huellitas.Business.Notifications;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Interface of Notification Service
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>the value</returns>
        IList<Notification> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the notification</returns>
        Notification GetById(int id);

        /// <summary>
        /// Gets the cached notification.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>the value</returns>
        Notification GetCachedNotification(NotificationType type);

        /// <summary>
        /// Gets the cached notifications.
        /// </summary>
        /// <returns>the value</returns>
        IList<Notification> GetCachedNotifications();

        /// <summary>
        /// Gets the user notifications.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="size">The size.</param>
        /// <returns>the value</returns>
        IPagedList<SystemNotification> GetUserNotifications(int userID, int page, int size);

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the task</returns>
        Task NewNotification(User user, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters);

        /// <summary>
        /// Inserts the notification.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the task</returns>
        Task NewNotification(IList<User> users, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters);

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
        /// <returns>the task</returns>
        Task NewNotification(User user, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters, string defaultFromName, string defaultSubject, string defaultMessage);

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
        /// <returns>the task</returns>
        Task NewNotification(IList<User> users, User userTriggerEvent, NotificationType type, string targetUrl, IList<NotificationParameter> parameters, string defaultFromName, string defaultSubject, string defaultMessage);

        /// <summary>
        /// News the notification email.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="notificationType">Type of the notification.</param>
        /// <param name="to">To message</param>
        /// <returns>the task</returns>
        Task NewNotificationEmail(
            IList<NotificationParameter> parameters,
            NotificationType notificationType,
            string to);

        /// <summary>
        /// Get the seen notifications.
        /// </summary>
        /// <param name="notifications">The notifications.</param>
        /// <returns>the value</returns>
        Task SeenNotifications(int[] notifications);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        Task Update(Notification entity);
    }
}