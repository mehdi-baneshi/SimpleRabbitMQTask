using Microsoft.AspNetCore.Builder;
using SimpleRabbit.Infra.Ioc;
using SimpleRabbit.Subscriber;
using SimpleRabbit.Subscriber.DapperDataAccess;
using SimpleRabbit.Subscriber.RedisDataAccess;
using Serilog;

try
{
    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        DependencyContainer.RegisterServices(services);
        services.ConfigureDapperServices(hostContext.Configuration);
        services.ConfigureRedisServices(hostContext.Configuration);
        services.AddSingleton<Serilog.ILogger>(sp =>
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .CreateLogger();
        });

        services.AddHostedService<App>();
    })
    .UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration))
    .UseWindowsService()
    .Build();

    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Exception in application");
}
finally
{
    Log.CloseAndFlush();
}

