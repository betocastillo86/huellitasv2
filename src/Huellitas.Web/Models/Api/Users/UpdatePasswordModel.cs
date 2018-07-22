//-----------------------------------------------------------------------
// <copyright file="UpdatePasswordModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Users
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Update Password Model
    /// </summary>
    public class UpdatePasswordModel
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(500, MinimumLength = 6)]
        public string Password { get; set; }
    }
}