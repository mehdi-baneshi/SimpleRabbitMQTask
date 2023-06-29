using Microsoft.Extensions.DependencyInjection;
using SimpleRabbit.Core.Domain.Bus;
using SimpleRabbit.Infra.Bus;
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
        }
    }
}
