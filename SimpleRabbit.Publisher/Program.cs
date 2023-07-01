using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleRabbit.Infra.Ioc;
using SimpleRabbit.Publisher;

var host = CreateHostBuilder(args).Build();

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .ConfigureServices((_, services) =>
        {
            DependencyContainer.RegisterServices(services);
            services.AddHostedService<App>();
        });
}

host.Run();