using System;
using System.Threading;
using System.Threading.Tasks;
using Huellitas.Web.Infraestructure.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Huellitas.Web.BackgroundServices;

public class GenerateJavascriptConfigBackgroundService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;

    public GenerateJavascriptConfigBackgroundService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = this.serviceProvider.CreateScope();
        var javascriptConfigurationGenerator = scope.ServiceProvider.GetRequiredService<IJavascriptConfigurationGenerator>();
        javascriptConfigurationGenerator.CreateJavascriptConfigurationFile();
        return Task.CompletedTask;
    }
}