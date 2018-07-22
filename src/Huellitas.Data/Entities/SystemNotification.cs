//-----------------------------------------------------------------------
// <copyright file="SystemNotification.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using Beto.Core.Data.Notifications;

namespace Huellitas.Data.Entities
{
    /// <summary>
    /// System Notification
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public class SystemNotification : BaseEntity, ISystemNotificationEntity
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the target URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SystemNotification"/> is seen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if seen; otherwise, <c>false</c>.
        /// </value>
        public bool Seen { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public System.DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the trigger user identifier.
        /// </summary>
        /// <value>
        /// The trigger user identifier.
        /// </value>
        public int? TriggerUserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the trigger user.
        /// </summary>
        /// <value>
        /// The trigger user.
        /// </value>
        public virtual User TriggerUser { get; set; }
    }
}