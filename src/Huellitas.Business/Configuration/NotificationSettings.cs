//-----------------------------------------------------------------------
// <copyright file="NotificationSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Beto.Core.Data.Configuration;
    using Huellitas.Business.Extensions.Services;
    using Huellitas.Business.Services;

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
        private readonly ICoreSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public NotificationSettings(ICoreSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the body base HTML.
        /// </summary>
        /// <value>
        /// The body base HTML.
        /// </value>
        public string BodyBaseHtml { get { return this.settingService.Get<string>("NotificationSettings.BodyBaseHtml"); } }

        /// <summary>
        /// Gets the email sender email.
        /// </summary>
        /// <value>
        /// The email sender email.
        /// </value>
        public string EmailSenderEmail => this.settingService.Get<string>("NotificationSettings.EmailSenderEmail");

        /// <summary>
        /// Gets the name of the email sender.
        /// </summary>
        /// <value>
        /// The name of the email sender.
        /// </value>
        public string EmailSenderName { get { return this.settingService.Get<string>("NotificationSettings.EmailSenderName"); } }

        /// <summary>
        /// Gets the maximum attemtps to send email.
        /// </summary>
        /// <value>
        /// The maximum attemtps to send email.
        /// </value>
        public int MaxAttemtpsToSendEmail => this.settingService.Get<int>("NotificationSettings.MaxAttemtpsToSendEmail");

        /// <summary>
        /// Gets a value indicating whether [send email enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [send email enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool SendEmailEnabled => this.settingService.Get<bool>("NotificationSettings.SendEmailEnabled");

        /// <summary>
        /// Gets the SMTP host.
        /// </summary>
        /// <value>
        /// The SMTP host.
        /// </value>
        public string SmtpHost => this.settingService.Get<string>("NotificationSettings.SmtpHost");

        /// <summary>
        /// Gets the SMTP password.
        /// </summary>
        /// <value>
        /// The SMTP password.
        /// </value>
        public string SmtpPassword => this.settingService.Get<string>("NotificationSettings.SmtpPassword");

        /// <summary>
        /// Gets the SMTP port.
        /// </summary>
        /// <value>
        /// The SMTP port.
        /// </value>
        public int SmtpPort => this.settingService.Get<int>("NotificationSettings.SmtpPort");

        /// <summary>
        /// Gets the SMTP user.
        /// </summary>
        /// <value>
        /// The SMTP user.
        /// </value>
        public string SmtpUser => this.settingService.Get<string>("NotificationSettings.SmtpUser");

        /// <summary>
        /// Gets a value indicating whether [SMTP use SSL].
        /// </summary>
        /// <value>
        /// <c>true</c> if [SMTP use SSL]; otherwise, <c>false</c>.
        /// </value>
        public bool SmtpUseSsl => this.settingService.Get<bool>("NotificationSettings.SmtpUseSsl");

        /// <summary>
        /// Gets the take emails to send.
        /// </summary>
        /// <value>
        /// The take emails to send.
        /// </value>
        public int TakeEmailsToSend => this.settingService.Get<int>("NotificationSettings.TakeEmailsToSend");
    }
}