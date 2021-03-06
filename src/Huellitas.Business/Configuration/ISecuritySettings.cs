﻿//-----------------------------------------------------------------------
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

        /// <summary>
        /// Gets the maximum request file upload mb.
        /// </summary>
        /// <value>
        /// The maximum request file upload mb.
        /// </value>
        int MaxRequestFileUploadMB { get; }

        /// <summary>
        /// Gets a value indicating whether [track home requests].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [track home requests]; otherwise, <c>false</c>.
        /// </value>
        bool TrackHomeRequests { get; }
    }
}