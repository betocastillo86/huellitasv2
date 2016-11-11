//-----------------------------------------------------------------------
// <copyright file="AuthenticationUserModel.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Users
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Authentication User Model
    /// </summary>
    public class AuthenticationUserModel
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
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
