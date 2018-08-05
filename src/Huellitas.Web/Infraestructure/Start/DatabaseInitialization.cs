//-----------------------------------------------------------------------
// <copyright file="DatabaseInitialization.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using Huellitas.Data.Core;
    using Huellitas.Data.Migrations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Startup for database initialization
    /// </summary>
    public static class DatabaseInitialization
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="config">The configuration.</param>
        public static void InitDatabase(this IApplicationBuilder app, IHostingEnvironment env, IConfigurationRoot config)
        {
            var context = (HuellitasContext)app.ApplicationServices.GetService(typeof(HuellitasContext));
            context.Database.EnsureCreated();
            context.EnsureSeeding(config);
        }
    }
}