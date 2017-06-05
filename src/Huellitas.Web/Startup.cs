//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web
{
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
    using System.IO;

    /// <summary>
    /// The startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<HuellitasExceptionMiddleware>();

            app.AddHangFire(env, loggerFactory);

            app.UseMiddleware<CurrentDateMiddleware>();

            app.InitDatabase(env);

            ////app.UseDeveloperExceptionPage();

            app.AddJWTAuthorization(env, loggerFactory);

            //app.AddFacebookAuthorization();

            app.UseStaticFiles();

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
                    template: "",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "defaultRoute",
                    template: "{root:regex(^(?!api).+)}/{*complement}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            this.CreateJavascriptFile(app);

            app.StartRecurringJobs();

            ////loggerFactory.AddConsole();

            ////if (env.IsDevelopment())
            ////{
            ////    app.UseDeveloperExceptionPage();
            ////}

            ////app.Run(async (context) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World!");
            ////});
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            ////Habilita configuraciones con inyecciond e dependencia
            services.AddOptions();

            ////Agrega las opciones de cache
            services.AddMemoryCache();

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(WebApiExceptionAttribute));
            }).AddJsonOptions(c =>
            {
                c.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                c.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
            });

            ////Register all policies required
            services.ConfigurePolicies();

            ////Registra los Repositorios genericos
            services.RegisterHuellitasServices(configuration);

            //// External services
            services.RegisterHangFireServices(configuration);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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