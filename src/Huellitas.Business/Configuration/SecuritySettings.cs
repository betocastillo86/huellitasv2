//-----------------------------------------------------------------------
// <copyright file="SecuritySettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Huellitas.Business.Services.Configuration;

    /// <summary>
    /// Security Settings
    /// </summary>
    /// <seealso cref="Huellitas.Business.Configuration.ISecuritySettings" />
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1502:ElementMustNotBeOnSingleLine", Justification = "Reviewed.")]
    public class SecuritySettings : ISecuritySettings
    {
        /// <summary>
        /// The setting service
        /// </summary>
        private readonly ISystemSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecuritySettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public SecuritySettings(ISystemSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the authentication audience.
        /// </summary>
        /// <value>
        /// The authentication audience.
        /// </value>
        public string AuthenticationAudience { get { return this.settingService.GetCachedSetting<string>("SecuritySettings.AuthenticationAudience"); } }

        /// <summary>
        /// Gets the authentication issuer.
        /// </summary>
        /// <value>
        /// The authentication issuer.
        /// </value>
        public string AuthenticationIssuer { get { return this.settingService.GetCachedSetting<string>("SecuritySettings.AuthenticationIssuer"); } }

        /// <summary>
        /// Gets the authentication secret key.
        /// </summary>
        /// <value>
        /// The authentication secret key.
        /// </value>
        public string AuthenticationSecretKey { get { return this.settingService.GetCachedSetting<string>("SecuritySettings.AuthenticationSecretKey"); } }

        /// <summary>
        /// Gets the expiration token minutes.
        /// </summary>
        /// <value>
        /// The expiration token minutes.
        /// </value>
        public int ExpirationTokenMinutes { get { return this.settingService.GetCachedSetting<int>("SecuritySettings.ExpirationTokenMinutes"); } }
    }
}