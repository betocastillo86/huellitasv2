//-----------------------------------------------------------------------
// <copyright file="NotificationSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Huellitas.Business.Services.Configuration;

    /// <summary>
    /// Notification Settings
    /// </summary>
    /// <seealso cref="Huellitas.Business.Configuration.INotificationSettings" />
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1502:ElementMustNotBeOnSingleLine", Justification = "Reviewed.")]
    public class NotificationSettings : INotificationSettings
    {
        /// <summary>
        /// The setting service
        /// </summary>
        private readonly ISystemSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public NotificationSettings(ISystemSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the body base HTML.
        /// </summary>
        /// <value>
        /// The body base HTML.
        /// </value>
        public string BodyBaseHtml { get { return this.settingService.GetCachedSetting<string>("NotificationSettings.BodyBaseHtml"); } }

        /// <summary>
        /// Gets the name of the email sender.
        /// </summary>
        /// <value>
        /// The name of the email sender.
        /// </value>
        public string EmailSenderName { get { return this.settingService.GetCachedSetting<string>("NotificationSettings.EmailSenderName"); } }
    }
}