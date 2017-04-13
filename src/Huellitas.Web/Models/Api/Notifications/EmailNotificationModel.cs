//-----------------------------------------------------------------------
// <copyright file="EmailNotificationModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Huellitas.Web.Models.Api.Notifications
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Web.Models.Api.Common;

    /// <summary>
    /// Email Notification Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseModel" />
    public class EmailNotificationModel : BaseModel
    {
        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To email.
        /// </value>
        [Required]
        [EmailAddress]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the sent date.
        /// </summary>
        /// <value>
        /// The sent date.
        /// </value>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the sent tries.
        /// </summary>
        /// <value>
        /// The sent tries.
        /// </value>
        public short SentTries { get; set; }
    }
}