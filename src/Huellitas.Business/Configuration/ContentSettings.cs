//-----------------------------------------------------------------------
// <copyright file="ContentSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Huellitas.Business.Services.Configuration;

    /// <summary>
    /// Content Settings
    /// </summary>
    /// <seealso cref="Huellitas.Business.Configuration.IContentSettings" />
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1502:ElementMustNotBeOnSingleLine", Justification = "Reviewed.")]
    public class ContentSettings : IContentSettings
    {
        /// <summary>
        /// The setting service
        /// </summary>
        private readonly ISystemSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public ContentSettings(ISystemSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the default size of the page.
        /// </summary>
        /// <value>
        /// The default size of the page.
        /// </value>
        public int DefaultPageSize { get { return this.settingService.GetCachedSetting<int>("ContentSettings.DefaultPageSize"); } }
    }
}