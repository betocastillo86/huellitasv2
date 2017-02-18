//-----------------------------------------------------------------------
// <copyright file="GeneralSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Services.Configuration;

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
        private readonly ISystemSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public GeneralSettings(ISystemSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl { get { return this.settingService.GetCachedSetting<string>("GeneralSettings.SiteUrl"); } }

        /// <summary>
        /// Gets the facebook URL.
        /// </summary>
        /// <value>
        /// The facebook URL.
        /// </value>
        public string FacebookUrl { get { return this.settingService.GetCachedSetting<string>("GeneralSettings.FacebookUrl"); } }

        /// <summary>
        /// Gets the <c>instagram</c> URL.
        /// </summary>
        /// <value>
        /// The <c>instagram</c> URL.
        /// </value>
        public string InstagramUrl { get { return this.settingService.GetCachedSetting<string>("GeneralSettings.InstagramUrl"); } }
    }
}