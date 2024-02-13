//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;
using Huellitas.Web.BackgroundServices;
using Huellitas.Web.Serializers;

namespace Huellitas.Web
{
    using Beto.Core.Web.Middleware;
    using Huellitas.Web.Infraestructure.Filters;
    using Huellitas.Web.Infraestructure.Start;
    using Infraestructure.Middleware;
    using Infraestructure.UI;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;

    /// <summary>
    /// The startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The environment.</param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            this.Configuration = builder.Build();
            this.Environment = env;
        }

        public IConfigurationRoot Configuration { get; private set; }

        public IWebHostEnvironment Environment { get; set; }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionsMiddlewareLogger>();

            if (Convert.ToBoolean(this.Configuration["EnableHangfire"]))
            {
                app.AddHangFire(env, loggerFactory);
            }

            app.UseMiddleware<CurrentDateMiddleware>();

            if (env.IsEnvironment("Test"))
            {
                app.UseMiddleware<FakeRemoteIpAddressMiddleware>();
            }

            //app.InitDatabase(env, this.Configuration);

            ////app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.AddJWTAuthorization(env, loggerFactory);

            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });
            

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    name: "adminLoginRoute",
                    pattern: "admin/login",
                    defaults: new { controller = "Admin", action = "Login" });
            
                routes.MapControllerRoute(
                    name: "defaultAdminRoute",
                    pattern: "admin/{*complement}",
                    defaults: new { controller = "Admin", action = "Index" });
            
                routes.MapControllerRoute(
                    name: "HomeRoute",
                    pattern: string.Empty,
                    defaults: new { controller = "Home", action = "Index" });
            
                routes.MapControllerRoute(
                    name: "PreviousURLs",
                    pattern: "fundaciones/{id:int}/{name}",
                    defaults: new { controller = "Home", action = "RedirectPrevious" });
            
                routes.MapControllerRoute(
                    name: "defaultRoute",
                    pattern: "{root:regex(^(?!api).+)}/{*complement}",
                    defaults: new { controller = "Home", action = "Index" });
            });
            
            if (!env.IsEnvironment("Test"))
            {
                app.StartRecurringJobs(this.Configuration);
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ////Habilita configuraciones con inyecciond e dependencia
            services.AddOptions();


            ////Agrega las opciones de cache
            services.AddMemoryCache();

            
            services.AddControllersWithViews(config =>
            {
                config.Filters.Add(typeof(WebApiExceptionAttribute));
            })
            .AddNewtonsoftJson(c =>
            {
                c.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                c.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
            });
            
            ////Registra los Repositorios genericos
            services.RegisterHuellitasServices(this.Configuration, this.Environment);

            if (Convert.ToBoolean(this.Configuration["EnableHangfire"]))
            {
                //// External services
                services.RegisterHangFireServices(this.Configuration);
            }

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ////Register all policies required
            services.AddJwtAuthentication(this.Environment);

            services.AddHostedService<GenerateJavascriptConfigBackgroundService>();
        }

        /// <summary>
        /// Creates the <c>javascript</c> file.
        /// </summary>
        /// <param name="serviceProvider">The builder.</param>
        private void CreateJavascriptFile(ServiceProvider serviceProvider)
        {
            var javascriptGenerator = (IJavascriptConfigurationGenerator)serviceProvider.GetService(typeof(IJavascriptConfigurationGenerator));
            javascriptGenerator.CreateJavascriptConfigurationFile();
        }
    }
}