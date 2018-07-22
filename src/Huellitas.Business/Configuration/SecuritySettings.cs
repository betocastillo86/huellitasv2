//-----------------------------------------------------------------------
// <copyright file="SecuritySettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Beto.Core.Data.Configuration;
    using Huellitas.Business.Extensions.Services;

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
        private readonly ICoreSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecuritySettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public SecuritySettings(ICoreSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the authentication audience.
        /// </summary>
        /// <value>
        /// The authentication audience.
        /// </value>
        public string AuthenticationAudience { get { return this.settingService.Get<string>("SecuritySettings.AuthenticationAudience"); } }

        /// <summary>
        /// Gets the authentication issuer.
        /// </summary>
        /// <value>
        /// The authentication issuer.
        /// </value>
        public string AuthenticationIssuer { get { return this.settingService.Get<string>("SecuritySettings.AuthenticationIssuer"); } }

        /// <summary>
        /// Gets the authentication secret key.
        /// </summary>
        /// <value>
        /// The authentication secret key.
        /// </value>
        public string AuthenticationSecretKey { get { return this.settingService.Get<string>("SecuritySettings.AuthenticationSecretKey"); } }

        /// <summary>
        /// Gets the expiration token minutes.
        /// </summary>
        /// <value>
        /// The expiration token minutes.
        /// </value>
        public int ExpirationTokenMinutes { get { return this.settingService.Get<int>("SecuritySettings.ExpirationTokenMinutes"); } }

        /// <summary>
        /// Gets the maximum request file upload mb.
        /// </summary>
        /// <value>
        /// The maximum request file upload mb.
        /// </value>
        public int MaxRequestFileUploadMB { get { return this.settingService.Get<int>("SecuritySettings.MaxRequestFileUploadMB"); } }

        /// <summary>
        /// Gets a value indicating whether [track home requests].
        /// </summary>
        /// <value>
        /// <c>true</c> if [track home requests]; otherwise, <c>false</c>.
        /// </value>
        public bool TrackHomeRequests => this.settingService.Get<bool>("SecuritySettings.TrackHomeRequests");
    }
}