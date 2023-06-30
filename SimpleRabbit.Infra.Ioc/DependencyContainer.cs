using Microsoft.Extensions.DependencyInjection;
using SimpleRabbit.Core.Domain.Bus;
using SimpleRabbit.Infra.Bus;
using SimpleRabbit.Subscriber.Application.EventHandlers;
using SimpleRabbit.Subscriber.Application.Events;
using SimpleRabbit.Subscriber.Application.Services;
using SimpleRabbit.Subscriber.DapperDataAccess;
using SimpleRabbit.Subscriber.DapperDataAccess.Migrations;
using SimpleRabbit.Subscriber.DapperDataAccess.Repository;
using SimpleRabbit.Subscriber.Domain.Interfaces.Repositories;
using SimpleRabbit.Subscriber.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp => new RabbitMQBus(sp.GetRequiredService<IServiceScopeFactory>()));

            //Subscriptions
            services.AddTransient<PersonEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<PersonCreatedEvent>, PersonEventHandler>();

            //Application Services
            services.AddTransient<IPersonService, PersonService>();

            //Data
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();
        }
    }
}
