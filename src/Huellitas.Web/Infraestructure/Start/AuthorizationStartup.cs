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
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
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
        public static void AddJWTAuthorization(this IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            ////////Guia tomada de https://stormpath.com/blog/token-authentication-asp-net-core
            ////var securitySettings = (ISecuritySettings)app.ApplicationServices.GetService(typeof(ISecuritySettings));

            ////var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securitySettings.AuthenticationSecretKey));

            ////var validationParameters = new TokenValidationParameters
            ////{
            ////    ValidAudience = securitySettings.AuthenticationAudience,
            ////    ValidateIssuer = true,
            ////    IssuerSigningKey = signingKey,
            ////    ValidIssuer = securitySettings.AuthenticationIssuer,
            ////    ValidateLifetime = true
            ////};

            ////app.UseJwtBearerAuthentication(new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions()
            ////{
            ////    AutomaticAuthenticate = true,
            ////    AutomaticChallenge = true,
            ////    TokenValidationParameters = validationParameters
            ////});
            app.UseAuthentication();

            app.UseAuthorization();
        }

        /// <summary>
        /// Configures the policies.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddJwtAuthentication(this IServiceCollection services, IWebHostEnvironment env)
        {
            string authenticationAudience = "AudienceAuthentication";
            string authenticationIssuer = "AuthenticationIssuer";
            string authenticationSecretKey = "TheSecretKey132456789";

            if (!env.IsEnvironment("Test"))
            {
                var sp = services.BuildServiceProvider();

                var securitySettings = sp.GetService<ISecuritySettings>();

                if (securitySettings.AuthenticationSecretKey == null)
                {
                    return;
                }

                authenticationAudience = securitySettings.AuthenticationAudience;
                authenticationIssuer = securitySettings.AuthenticationIssuer;
                authenticationSecretKey = securitySettings.AuthenticationSecretKey;
            }

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationSecretKey));

            var validationParameters = new TokenValidationParameters
            {
                ValidAudience = authenticationAudience,
                ValidateIssuer = true,
                IssuerSigningKey = signingKey,
                ValidIssuer = authenticationIssuer,
                ValidateLifetime = true
            };

            services.AddAuthentication(c =>
            {
                c.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(c =>
            {
                c.Audience = authenticationAudience;
                c.ClaimsIssuer = authenticationIssuer;
                c.TokenValidationParameters = validationParameters;
            });

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