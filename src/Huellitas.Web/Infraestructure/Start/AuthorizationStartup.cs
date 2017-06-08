//-----------------------------------------------------------------------
// <copyright file="AuthorizationStartup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using System.Security.Claims;
    using System.Text;
    using Business.Configuration;
    using Data.Entities;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

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

        /// <summary>
        /// Configures the policies.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigurePolicies(this IServiceCollection services)
        {
            services.AddAuthorization(c =>
            {
                c.AddPolicy(
                    "IsAdmin", 
                    policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, RoleEnum.SuperAdmin.ToString(), "Admin");
                });
            });
        }
    }
}