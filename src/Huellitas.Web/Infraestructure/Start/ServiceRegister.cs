using Huellitas.Business.Helpers;
using Huellitas.Business.Services.Common;
using Huellitas.Business.Services.Contents;
using Huellitas.Data.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Infraestructure.Start
{
    public static class ServiceRegister
    {
        public static void RegisterHuellitasServices(this IServiceCollection services)
        {
            //Registra los Repositorios genericos
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<IHttpContextHelpers, HttpContextHelpers>();

            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ILogService, LogService>();
        }
    }
}
