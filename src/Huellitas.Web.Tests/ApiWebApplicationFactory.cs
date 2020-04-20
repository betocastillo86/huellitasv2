using Beto.Core.Data;
using Huellitas.Data.Core;
using Huellitas.Data.Migrations;
using Huellitas.Web.Tests.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Huellitas.Web.Tests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<HuellitasContext>(options =>
                {
                    options.UseInMemoryDatabase("Huellitas");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                services.AddScoped<IDbContext>(provider => provider.GetService<HuellitasContext>());

                var scopeServiceProvider = services.BuildServiceProvider();
                using var scope = scopeServiceProvider.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<HuellitasContext>();
                var logger = scopedServices.GetRequiredService<ILogger<ApiWebApplicationFactory>>();

                context.Database.EnsureCreated();
                this.SeedTestData(context);
            })
                .UseEnvironment("Test");
        }

        private void SeedTestData(HuellitasContext context)
        {
            EnsureSeedingExtension.Seed(context);
        }
    }
}