//-----------------------------------------------------------------------
// <copyright file="IGeneralSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    /// <summary>
    /// Interface of General Settings
    /// </summary>
    public interface IGeneralSettings
    {
        /// <summary>
        /// Gets the default size of the page.
        /// </summary>
        /// <value>
        /// The default size of the page.
        /// </value>
        int DefaultPageSize { get; }

        /// <summary>
        /// Gets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        string SiteUrl { get; }

        /// <summary>
        /// Gets the facebook URL.
        /// </summary>
        /// <value>
        /// The facebook URL.
        /// </value>
        string FacebookUrl { get; }

        /// <summary>
        /// Gets the <c>instagram</c> URL.
        /// </summary>
        /// <value>
        /// The <c>instagram</c> URL.
        /// </value>
        string InstagramUrl { get; }

        /// <summary>
        /// Gets the width of the banner picture size.
        /// </summary>
        /// <value>
        /// The width of the banner picture size.
        /// </value>
        int BannerPictureSizeWidth { get; }

        /// <summary>
        /// Gets the height of the banner picture size.
        /// </summary>
        /// <value>
        /// The height of the banner picture size.
        /// </value>
        int BannerPictureSizeHeight { get; }
    }
}