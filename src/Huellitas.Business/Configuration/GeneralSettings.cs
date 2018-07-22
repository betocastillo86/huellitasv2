//-----------------------------------------------------------------------
// <copyright file="GeneralSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Beto.Core.Data.Configuration;
    using Huellitas.Business.Extensions.Services;
    using Services;

    /// <summary>
    /// General Settings
    /// </summary>
    /// <seealso cref="Huellitas.Business.Configuration.IGeneralSettings" />
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1502:ElementMustNotBeOnSingleLine", Justification = "Reviewed.")]
    public class GeneralSettings : IGeneralSettings
    {
        /// <summary>
        /// The setting service
        /// </summary>
        private readonly ICoreSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public GeneralSettings(ICoreSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the height of the banner picture size.
        /// </summary>
        /// <value>
        /// The height of the banner picture size.
        /// </value>
        public int BannerPictureSizeHeight { get { return this.settingService.Get<int>("GeneralSettings.BannerPictureSizeHeight"); } }

        /// <summary>
        /// Gets the width of the banner picture size.
        /// </summary>
        /// <value>
        /// The width of the banner picture size.
        /// </value>
        public int BannerPictureSizeWidth { get { return this.settingService.Get<int>("GeneralSettings.BannerPictureSizeWidth"); } }

        /// <summary>
        /// Gets the configuration JAVASCRIPT cache key.
        /// </summary>
        /// <value>
        /// The configuration JAVASCRIPT cache key.
        /// </value>
        public string ConfigJavascriptCacheKey { get { return this.settingService.Get<string>("GeneralSettings.ConfigJavascriptCacheKey"); } }

        /// <summary>
        /// Gets the default size of the page.
        /// </summary>
        /// <value>
        /// The default size of the page.
        /// </value>
        public int DefaultPageSize { get { return this.settingService.Get<int>("GeneralSettings.DefaultPageSize"); } }

        /// <summary>
        /// Gets the facebook public token.
        /// </summary>
        /// <value>
        /// The facebook public token.
        /// </value>
        public string FacebookPublicToken { get { return this.settingService.Get<string>("GeneralSettings.FacebookPublicToken"); } }

        /// <summary>
        /// Gets the facebook secret token.
        /// </summary>
        /// <value>
        /// The facebook secret token.
        /// </value>
        public string FacebookSecretToken { get { return this.settingService.Get<string>("GeneralSettings.FacebookSecretToken"); } }

        /// <summary>
        /// Gets the facebook URL.
        /// </summary>
        /// <value>
        /// The facebook URL.
        /// </value>
        public string FacebookUrl { get { return this.settingService.Get<string>("GeneralSettings.FacebookUrl"); } }

        /// <summary>
        /// Gets the google analytics code.
        /// </summary>
        /// <value>
        /// The google analytics code.
        /// </value>
        public string GoogleAnalyticsCode => this.settingService.Get<string>("GeneralSettings.GoogleAnalyticsCode");

        /// <summary>
        /// Gets the <c>instagram</c> URL.
        /// </summary>
        /// <value>
        /// The <c>instagram</c> URL.
        /// </value>
        public string InstagramUrl { get { return this.settingService.Get<string>("GeneralSettings.InstagramUrl"); } }

        /// <summary>
        /// Gets the maximum size of the height picture.
        /// </summary>
        /// <value>
        /// The maximum size of the height picture.
        /// </value>
        public int MaxHeightPictureSize => this.settingService.Get<int>("GeneralSettings.MaxHeightPictureSize");

        /// <summary>
        /// Gets the maximum size of the with picture.
        /// </summary>
        /// <value>
        /// The maximum size of the with picture.
        /// </value>
        public int MaxWidthPictureSize => this.settingService.Get<int>("GeneralSettings.MaxWidthPictureSize");

        /// <summary>
        /// Gets the SEO image.
        /// </summary>
        /// <value>
        /// The SEO image.
        /// </value>
        public string SeoImage { get { return this.settingService.Get<string>("GeneralSettings.SeoImage"); } }

        /// <summary>
        /// Gets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl { get { return this.settingService.Get<string>("GeneralSettings.SiteUrl"); } }

        /// <summary>
        /// Gets a value indicating whether [adsense enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [adsense enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool AdsenseEnabled { get { return this.settingService.Get<bool>("GeneralSettings.AdsenseEnabled"); } }
    }
}