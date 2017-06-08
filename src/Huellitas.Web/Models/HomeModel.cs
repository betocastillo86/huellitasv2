//-----------------------------------------------------------------------
// <copyright file="HomeModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models
{
    /// <summary>
    /// Home Model
    /// </summary>
    public class HomeModel
    {
        /// <summary>
        /// Gets or sets the cache key.
        /// </summary>
        /// <value>
        /// The cache key.
        /// </value>
        public string CacheKey { get; set; }

        /// <summary>
        /// Gets or sets the google analytics code.
        /// </summary>
        /// <value>
        /// The google analytics code.
        /// </value>
        public string GoogleAnalyticsCode { get; set; }
    }
}