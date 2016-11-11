//-----------------------------------------------------------------------
// <copyright file="AuthorizationStartup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using System.Text;
    using Business.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Security;

    /// <summary>
    /// Adds the authorization startup configuration
    /// </summary>
    public static class AuthorizationStartup
    {
        /// <summary>
        /// Adds the authorization.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public static void AddJWTAuthorization(this IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            ////Guia tomada de https://stormpath.com/blog/token-authentication-asp-net-core
            var securitySettings = (ISecuritySettings)app.ApplicationServices.GetService(typeof(ISecuritySettings));

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securitySettings.AuthenticationSecretKey));

            var validationParameters = new TokenValidationParameters
            {
                ValidAudience = securitySettings.AuthenticationAudience,
                ValidateIssuer = true,
                IssuerSigningKey = signingKey,
                ValidIssuer = securitySettings.AuthenticationIssuer,
                ValidateLifetime = true
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = validationParameters
            });
        }
    }
}