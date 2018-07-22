//-----------------------------------------------------------------------
// <copyright file="ServiceRegister.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using System;
    using System.Reflection;
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Beto.Core.Data.Common;
    using Beto.Core.Data.Configuration;
    using Beto.Core.Data.Files;
    using Beto.Core.Data.Notifications;
    using Beto.Core.Data.Users;
    using Beto.Core.EventPublisher;
    using Beto.Core.Exceptions;
    using Beto.Core.Helpers;
    using Beto.Core.Registers;
    using Beto.Core.Web.Security;
    using Business.Configuration;
    using Business.Security;
    using Business.Services;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Tasks;
    using Huellitas.Data.Core;
    using Huellitas.Web.Infraestructure.Filters.Action;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Security;
    using UI;

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
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IDbContext, HuellitasContext>();

            services.AddTransient<IHttpContextHelper, HttpContextHelper>();

            ////Core
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IAuthenticationTokenGenerator, AuthenticationTokenGeneratorJWT>();
            services.AddScoped<IWorkContext, WorkContext>();

            ////Settings
            services.AddScoped<IContentSettings, ContentSettings>();
            services.AddScoped<IGeneralSettings, GeneralSettings>();
            services.AddScoped<INotificationSettings, Huellitas.Business.Configuration.NotificationSettings>();
            services.AddScoped<ISecuritySettings, SecuritySettings>();
            services.AddScoped<ITaskSettings, TaskSettings>();

            ////Services
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ILoggerService, LogService>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddScoped<ISeoService, SeoService>();
            services.AddScoped<ICoreSettingService, CoreSettingService>();
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
            services.AddScoped<IServiceFactory, DefaultServiceFactory>();
            services.AddScoped<IMessageExceptionFinder, MessageExceptionFinder>();
            services.AddScoped<ISeoHelper, SeoHelper>();
            services.AddScoped<ICoreNotificationService, CoreNotificationService>();
            services.AddScoped<ISocialAuthenticationService, SocialAuthenticationService>();
            services.AddScoped<ICorePictureResizerService, PictureResizer>();

            // Filters
            services.AddScoped<CrawlerAttribute>();

            ////Events
            //services.AddScoped<ImageResizeTask, ImageResizeTask>();

            foreach (var implementationType in ReflectionHelper.GetTypesOnProject(typeof(ISubscriber<>), "huellitas"))
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

            foreach (var implementationType in ReflectionHelper.GetTypesOnProject(typeof(ITask), "huellitas"))
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