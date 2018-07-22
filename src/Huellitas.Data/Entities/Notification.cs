//-----------------------------------------------------------------------
// <copyright file="Notification.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using Beto.Core.Data.Notifications;

    /// <summary>
    /// Notification Class
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class Notification : BaseEntity, INotificationEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the system text.
        /// </summary>
        /// <value>
        /// The system text.
        /// </value>
        public string SystemText { get; set; }

        /// <summary>
        /// Gets or sets the email HTML.
        /// </summary>
        /// <value>
        /// The email HTML.
        /// </value>
        public string EmailHtml { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        /// <value>
        /// The email subject.
        /// </value>
        public string EmailSubject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is system.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is system; otherwise, <c>false</c>.
        /// </value>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is email.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is email; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmail { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Notification"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Notification"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        public System.DateTime? UpdateDate { get; set; }
    }
}