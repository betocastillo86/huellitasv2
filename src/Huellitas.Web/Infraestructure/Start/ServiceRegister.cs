//-----------------------------------------------------------------------
// <copyright file="ServiceRegister.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using System.IO;
    using Business.Services.Files;
    using Business.Caching;
    using Business.Configuration;
    using Business.Services.Configuration;
    using Business.Services.Seo;
    using Huellitas.Business.Helpers;
    using Huellitas.Business.Services.Common;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Data.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Security;
    using Business.Services.Users;
    using Business.Security;

    /// <summary>
    /// Helper for register services
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// Registers the <![CDATA[huellitas]]> services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterHuellitasServices(this IServiceCollection services)
        {
            ////Registra el contexto de base de datos
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();
            services.AddDbContext<HuellitasContext>(options => options.UseSqlServer(connectionStringConfig.GetConnectionString("DefaultConnection")));

            ////Registra los Repositorios genericos
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<IHttpContextHelpers, HttpContextHelpers>();

            ////Core
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IAuthenticationTokenGenerator, AuthenticationTokenGeneratorJWT>();
            services.AddScoped<IWorkContext, WorkContext>();

            ////Settings
            services.AddScoped<IContentSettings, ContentSettings>();
            services.AddScoped<ISecuritySettings, SecuritySettings>();

            ////Helpers
            services.AddScoped<ISecurityHelpers, SecurityHelpers>();

            ////Services
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ISeoService, SeoService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ICustomTableService, CustomTableService>();
            services.AddScoped<IFilesHelper, FilesHelper>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}