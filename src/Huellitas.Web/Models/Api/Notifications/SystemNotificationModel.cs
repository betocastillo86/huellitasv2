//-----------------------------------------------------------------------
// <copyright file="SystemNotificationModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Notifications
{
    using System;
    using Huellitas.Web.Models.Api.Common;

    /// <summary>
    /// System Notification Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseModel" />
    public class SystemNotificationModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the target URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SystemNotificationModel"/> is seen.
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
        public DateTime CreationDate { get; set; }
    }
}