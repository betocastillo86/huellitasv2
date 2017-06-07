﻿//-----------------------------------------------------------------------
// <copyright file="HangfireStartup.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Start
{
    using System;
    using System.IO;
    using Hangfire;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Tasks;
    using Huellitas.Web.Infraestructure.Filters.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Initializes and registers hangfire
    /// </summary>
    public static class HangfireStartup
    {
        /// <summary>
        /// Adds the hang fire.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public static void AddHangFire(this IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();

            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionStringConfig.GetConnectionString("HangfireConnection"));

            var dashboardOptions = new DashboardOptions()
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            };

            app.UseHangfireDashboard(options: dashboardOptions);
            app.UseHangfireServer();
        }

        /// <summary>
        /// Registers the hang fire services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterHangFireServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddHangfire(c => c.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));
        }

        /// <summary>
        /// Starts the recurring jobs.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void StartRecurringJobs(this IApplicationBuilder app)
        {
            var settings = (ITaskSettings)app.ApplicationServices.GetService(typeof(ITaskSettings));
            if (settings.SendEmailsInterval > 0)
            {
                RecurringJob.AddOrUpdate<SendMailTask>(c => c.SendPendingMails(), Cron.Minutely());
            }
        }
    }
}