//-----------------------------------------------------------------------
// <copyright file="PasswordRecoveryModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Users
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Password Recovery Model
    /// </summary>
    public class PasswordRecoveryModel
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}