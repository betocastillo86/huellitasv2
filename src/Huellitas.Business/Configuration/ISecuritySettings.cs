//-----------------------------------------------------------------------
// <copyright file="ISecuritySettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    /// <summary>
    /// Interface of security settings
    /// </summary>
    public interface ISecuritySettings
    {
        /// <summary>
        /// Gets the authentication audience.
        /// </summary>
        /// <value>
        /// The authentication audience.
        /// </value>
        string AuthenticationAudience { get; }

        /// <summary>
        /// Gets the authentication issuer.
        /// </summary>
        /// <value>
        /// The authentication issuer.
        /// </value>
        string AuthenticationIssuer { get; }

        /// <summary>
        /// Gets the authentication secret key.
        /// </summary>
        /// <value>
        /// The authentication secret key.
        /// </value>
        string AuthenticationSecretKey { get; }

        /// <summary>
        /// Gets the expiration token minutes.
        /// </summary>
        /// <value>
        /// The expiration token minutes.
        /// </value>
        int ExpirationTokenMinutes { get; }

        int MaxRequestFileUploadMB { get; }
    }
}