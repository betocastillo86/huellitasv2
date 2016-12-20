//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web
{
    using Huellitas.Web.Infraestructure.Filters.Exceptions;
    using Huellitas.Web.Infraestructure.Start;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;

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
            app.InitDatabase(env);

            ////app.UseDeveloperExceptionPage();

            app.AddJWTAuthorization(env, loggerFactory);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "adminLoginRoute",
                    template: "admin/login",
                    defaults: new {  controller = "Admin", action ="Login"});

                routes.MapRoute(
                    name: "defaultAdminRoute",
                    template: "admin/{*complement}",
                    defaults:new { controller = "Admin", action = "Index" });

                routes.MapRoute(
                    name: "defaultRoute",
                    template: "{controller=Home}/{action=Index}");
            });

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
            ////Habilita configuraciones con inyecciond e dependencia
            services.AddOptions();

            ////Agrega las opciones de cache
            services.AddMemoryCache();

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(WebApiExceptionAttribute));
            }).AddJsonOptions(c => c.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat);

            ////Register all policies required
            services.ConfigurePolicies();

            ////Registra los Repositorios genericos
            services.RegisterHuellitasServices();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}