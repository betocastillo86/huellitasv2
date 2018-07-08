﻿//-----------------------------------------------------------------------
// <copyright file="ServiceRegister.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using System;
    using System.IO;
    using System.Reflection;
    using Business.Caching;
    using Business.Configuration;
    using Business.EventPublisher;
    using Business.Security;
    using Business.Services;
    using Huellitas.Business.Helpers;
    using Huellitas.Data.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Security;
    using UI;
    using Huellitas.Web.Infraestructure.Tasks;
    using Huellitas.Business.Tasks;
    using Huellitas.Web.Infraestructure.Filters.Action;

    /// <summary>
    /// Helper for register services
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// Registers the <![CDATA[huellitas]]> services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterHuellitasServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            ///////Registra el contexto de base de datos
            ///var builder = new ConfigurationBuilder();
            ///builder.SetBasePath(Directory.GetCurrentDirectory());
            ///builder.AddJsonFile("appsettings.json");
            ///
            ///var connectionStringConfig = builder.Build();
            services.AddDbContext<HuellitasContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            ////Registra los Repositorios genericos
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<IHttpContextHelpers, HttpContextHelpers>();

            ////Core
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IAuthenticationTokenGenerator, AuthenticationTokenGeneratorJWT>();
            services.AddScoped<IWorkContext, WorkContext>();

            ////Settings
            services.AddScoped<IContentSettings, ContentSettings>();
            services.AddScoped<IGeneralSettings, GeneralSettings>();
            services.AddScoped<INotificationSettings, NotificationSettings>();
            services.AddScoped<ISecuritySettings, SecuritySettings>();
            services.AddScoped<ITaskSettings, TaskSettings>();

            ////Helpers
            services.AddScoped<ISecurityHelpers, SecurityHelpers>();
            services.AddSingleton<IStringHelpers, StringHelpers>();

            ////Services
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ISeoService, SeoService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<ICustomTableService, CustomTableService>();
            services.AddScoped<IFilesHelper, FilesHelper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAdoptionFormService, AdoptionFormService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ITextResourceService, TextResourceService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IExternalAuthenticationService, ExternalAuthenticationService>();
            services.AddScoped<ICrawlingService, CrawlingService>();

            ////UI
            services.AddScoped<IJavascriptConfigurationGenerator, JavascriptConfigurationGenerator>();

            ////Events
            services.AddScoped<IPublisher, Publisher>();

            // Filters
            services.AddScoped<CrawlerAttribute>();

            ////Events
            //services.AddScoped<ImageResizeTask, ImageResizeTask>();

            foreach (var implementationType in ReflectionHelpers.GetTypesOnProject(typeof(ISubscriber<>)))
            {
                var servicesTypeFound = implementationType.GetTypeInfo().FindInterfaces(
                    (type, criteria) =>
                {
                    return type.GetTypeInfo().IsGenericType && ((Type)criteria).GetTypeInfo().IsAssignableFrom(type.GetGenericTypeDefinition());
                },
                    typeof(ISubscriber<>));

                foreach (var serviceFoundType in servicesTypeFound)
                {
                    services.AddScoped(serviceFoundType, implementationType);
                }
            }

            foreach (var implementationType in ReflectionHelpers.GetTypesOnProject(typeof(ITask)))
            {
                var servicesTypeFound = implementationType.GetTypeInfo().FindInterfaces(
                    (type, criteria) =>
                    {
                        return true;
                    },
                    typeof(ITask));

                foreach (var serviceFoundType in servicesTypeFound)
                {
                    services.AddScoped(implementationType, implementationType);
                }
            }
        }
    }
}