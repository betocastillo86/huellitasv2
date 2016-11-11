//-----------------------------------------------------------------------
// <copyright file="ServiceRegister.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using Business.Services.Files;
    using Business.Services.Seo;
    using Huellitas.Business.Helpers;
    using Huellitas.Business.Services.Common;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Data.Core;
    using Microsoft.Extensions.DependencyInjection;

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
            ////Registra los Repositorios genericos
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<IHttpContextHelpers, HttpContextHelpers>();

            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ISeoService, SeoService>();
            services.AddScoped<ICustomTableService, CustomTableService>();
            services.AddScoped<IFilesHelper, FilesHelper>();
        }
    }
}