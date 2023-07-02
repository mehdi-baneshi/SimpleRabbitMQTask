using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleRabbit.Core.Domain.Bus;
using SimpleRabbit.Infra.Bus;
using SimpleRabbit.Subscriber.Application.EventHandlers;
using SimpleRabbit.Subscriber.Application.Events;
using SimpleRabbit.Subscriber.Application.Services;
using SimpleRabbit.Subscriber.Application.Validators;
using SimpleRabbit.Subscriber.DapperDataAccess;
using SimpleRabbit.Subscriber.DapperDataAccess.Migrations;
using SimpleRabbit.Subscriber.DapperDataAccess.Repository;
using SimpleRabbit.Subscriber.Domain.Dtos.Person;
using SimpleRabbit.Subscriber.Domain.Interfaces.Redis;
using SimpleRabbit.Subscriber.Domain.Interfaces.Repositories;
using SimpleRabbit.Subscriber.Domain.Interfaces.Services;
using SimpleRabbit.Subscriber.RedisDataAccess.RedisService;
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
            services.AddSingleton<IEventBus, RabbitMQBus>(sp => new RabbitMQBus(sp.GetRequiredService<IServiceScopeFactory>(),sp.GetRequiredService<ILogger<RabbitMQBus>>()));

            //Subscriptions
            services.AddTransient<PersonEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<PersonCreatedEvent>, PersonEventHandler>();

            //Application Services
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IValidator<CreatePersonDto>, CreatePersonDtoValidator>();

            //Data
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPersonRedisService, PersonRedisService>();
            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();
        }
    }
}
