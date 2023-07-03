using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleRabbit.Core.Domain.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Publisher
{
    public class App : BackgroundService
    {
        private readonly IEventBus _bus;
        private readonly List<Personn> _persons;
        private readonly ILogger<App> _logger;

        public App(IEventBus bus, ILogger<App> logger)
        {
            _bus = bus;
            _logger = logger;
            _persons = new List<Personn>();
        }

        private void SetSamplePersons()
        {
            var person1 = new Personn("Ahmad", "Majlesara", -35);
            var person2 = new Personn("Borna", "Shayesteh", 30);
            var person3 = new Personn("Ghasem", "Mokhaberati", 50);
            var person4 = new Personn("Panjali", "", 90);
            var person5 = new Personn("Pouya", "Jalilvand", 25);

            _persons.Add(person1);
            _persons.Add(person2);
            _persons.Add(person3);
            _persons.Add(person4);
            _persons.Add(person5);
        }

        public async Task PublishWithDely()
        {
            SetSamplePersons();

            foreach (var person in _persons)
            {
                _logger.LogInformation($"Sending of a message (person: {person.FirstName} {person.LastName}) to RabbitMQ started...");
                _bus.Publish(new SimpleRabbit.Publisher.Event.PersonCreatedEvent(person.FirstName, person.LastName, person.Age));
                await Task.Delay(15000);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async delegate
            {
                await PublishWithDely();
            }).Wait();

            Console.ReadLine();

            await Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending persons started");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending persons finished");
            return base.StopAsync(cancellationToken);
        }
    }

    public class Personn
    {
        public Personn(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
    }
}
