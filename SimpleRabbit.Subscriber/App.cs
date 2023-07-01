using SimpleRabbit.Core.Domain.Bus;
using SimpleRabbit.Subscriber.Application.EventHandlers;
using SimpleRabbit.Subscriber.Application.Events;
using SimpleRabbit.Subscriber.DapperDataAccess.Extentions;
using System.Text;

namespace SimpleRabbit.Subscriber
{
    public class App : BackgroundService
    {
        private readonly IEventBus _bus;
        private readonly IHost _host;
        private readonly ILogger<App> _logger;

        public App(IEventBus bus, IHost host, ILogger<App> logger)
        {
            _bus = bus;
            _host = host;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _host.MigrateDatabase();
                _bus.Subscribe<PersonCreatedEvent, PersonEventHandler>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }


            await Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("start");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("end");
            return base.StopAsync(cancellationToken);
        }
    }
}