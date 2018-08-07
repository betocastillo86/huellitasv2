//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web
{
    using System.IO;
    using Beto.Core.Web.Api.Filters;
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
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The startup
    /// </summary>
    public class Startup
    {
            /// <summary>
            /// Initializes a new instance of the <see cref="Startup"/> class.
            /// </summary>
            /// <param name="env">The environment.</param>
            public Startup(IHostingEnvironment env)
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

                this.Configuration = builder.Build();
            }

            /// <summary>
            /// Gets the configuration.
            /// </summary>
            /// <value>
            /// The configuration.
            /// </value>
            public IConfigurationRoot Configuration { get; private set; }

            /// <summary>
            /// Configures the specified application.
            /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            /// </summary>
            /// <param name="app">The application.</param>
            /// <param name="env">The env.</param>
            /// <param name="loggerFactory">The logger factory.</param>
            public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionsMiddlewareLogger>();

            app.AddHangFire(env, loggerFactory);

            app.UseMiddleware<CurrentDateMiddleware>();

            app.InitDatabase(env, this.Configuration);

            ////app.UseDeveloperExceptionPage();

            app.AddJWTAuthorization(env, loggerFactory);

            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "adminLoginRoute",
                    template: "admin/login",
                    defaults: new { controller = "Admin", action = "Login" });

                routes.MapRoute(
                    name: "defaultAdminRoute",
                    template: "admin/{*complement}",
                    defaults: new { controller = "Admin", action = "Index" });

                routes.MapRoute(
                    name: "HomeRoute",
                    template: string.Empty,
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "PreviousURLs",
                    template: "fundaciones/{id:int}/{name}",
                    defaults: new { controller = "Home", action = "RedirectPrevious" });

                routes.MapRoute(
                    name: "defaultRoute",
                    template: "{root:regex(^(?!api).+)}/{*complement}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            this.CreateJavascriptFile(app);

            app.StartRecurringJobs();
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

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(WebApiExceptionAttribute));
                config.Filters.Add(new FluentValidatorAttribute());
            }).AddJsonOptions(c =>
            {
                c.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                c.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
            });

            ////Registra los Repositorios genericos
            services.RegisterHuellitasServices(this.Configuration);

            //// External services
            services.RegisterHangFireServices(this.Configuration);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ////Register all policies required
            services.AddJwtAuthentication();
        }

        /// <summary>
        /// Creates the <c>javascript</c> file.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private void CreateJavascriptFile(IApplicationBuilder builder)
        {
            var javascriptGenerator = (IJavascriptConfigurationGenerator)builder.ApplicationServices.GetService(typeof(IJavascriptConfigurationGenerator));
            javascriptGenerator.CreateJavascriptConfigurationFile();
        }
    }
}