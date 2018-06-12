using System.ComponentModel.DataAnnotations;

namespace Huellitas.Web.Models
{
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