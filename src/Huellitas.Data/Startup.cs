//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data
{
    using System.IO;
    using Huellitas.Data.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Startup in Data for Migrations
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ////Habilita configuraciones con inyecciond e dependencia
            services.AddOptions();

            ////Se configura la cadena de conexión para poder hacer MIGRATIONS
            ////Otro enfoque podría ser http://stackoverflow.com/questions/29110241/how-do-you-configure-the-dbcontext-when-creating-migrations-in-entity-framework
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();
            services.AddDbContext<HuellitasContext>(options => options.UseSqlServer(connectionStringConfig.GetConnectionString("DefaultConnection")));
        }
    }
}