//-----------------------------------------------------------------------
// <copyright file="LogModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The Log Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseModel" />
    public class LogModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the short message.
        /// </summary>
        /// <value>
        /// The short message.
        /// </value>
        [Required]
        public string ShortMessage { get; set; }

        /// <summary>
        /// Gets or sets the full message.
        /// </summary>
        /// <value>
        /// The full message.
        /// </value>
        public string FullMessage { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user model.
        /// </summary>
        /// <value>
        /// The user model.
        /// </value>
        public BaseUserModel UserModel { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }
    }
}