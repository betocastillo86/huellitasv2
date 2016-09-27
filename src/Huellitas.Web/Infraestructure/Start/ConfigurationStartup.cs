//-----------------------------------------------------------------------
// <copyright file="ConfigurationStartup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using System.IO;
    using Huellitas.Data.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configurations for startup
    /// </summary>
    public static class ConfigurationStartup
    {
        /// <summary>
        /// Adds the configurations.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddConfigurations(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();
            services.AddDbContext<HuellitasContext>(options => options.UseSqlServer(connectionStringConfig.GetConnectionString("DefaultConnection")));
        }
    }
}