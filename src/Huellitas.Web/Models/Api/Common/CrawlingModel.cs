//-----------------------------------------------------------------------
// <copyright file="CrawlingModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Crawling Model
    /// </summary>
    public class CrawlingModel
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        /// <value>
        /// The HTML.
        /// </value>
        [Required]
        public string Html { get; set; }
    }
}