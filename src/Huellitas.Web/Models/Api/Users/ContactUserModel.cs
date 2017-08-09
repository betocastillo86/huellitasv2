//-----------------------------------------------------------------------
// <copyright file="ContactUserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Contact user model
    /// </summary>
    public class ContactUserModel
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Required]
        public string Message { get; set; }
    }
}