//-----------------------------------------------------------------------
// <copyright file="EmailNotificationFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Notifications
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Web.Models.Api.Common;

    /// <summary>
    /// Email Notification Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseFilterModel" />
    public class EmailNotificationFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationFilterModel"/> class.
        /// </summary>
        public EmailNotificationFilterModel()
        {
            this.MaxPageSize = 50;
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To email.
        /// </value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the sent.
        /// </summary>
        /// <value>
        /// The sent.
        /// </value>
        public bool? Sent { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}