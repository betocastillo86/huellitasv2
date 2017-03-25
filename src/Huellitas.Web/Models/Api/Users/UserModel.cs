﻿//-----------------------------------------------------------------------
// <copyright file="UserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Users
{
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Data.Entities.Enums;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// User Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Users.BaseUserModel" />
    public class UserModel : BaseUserModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleEnum? Role { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the second phone.
        /// </summary>
        /// <value>
        /// The second phone.
        /// </value>
        public string Phone2 { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the unseen notifications.
        /// </summary>
        /// <value>
        /// The unseen notifications.
        /// </value>
        public int UnseenNotifications { get; set; }
    }
}