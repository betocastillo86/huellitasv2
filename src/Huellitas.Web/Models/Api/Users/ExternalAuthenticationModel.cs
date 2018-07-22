//-----------------------------------------------------------------------
// <copyright file="ExternalAuthenticationModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Data.Entities;

    /// <summary>
    /// External authentication model
    /// </summary>
    public class ExternalAuthenticationModel
    {
        /// <summary>
        /// Gets or sets the social network.
        /// </summary>
        /// <value>
        /// The social network.
        /// </value>
        [Required]
        public SocialLoginType SocialNetwork { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the token2.
        /// </summary>
        /// <value>
        /// The token2.
        /// </value>
        public string Token2 { get; set; }
    }
}