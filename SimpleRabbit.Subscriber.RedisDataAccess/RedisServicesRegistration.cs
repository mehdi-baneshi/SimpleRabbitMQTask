using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.RedisDataAccess
{
    public static class RedisServicesRegistration
    {
        public static IServiceCollection ConfigureRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfig = configuration.GetValue<string>("RedisConnection");
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfig;
                options.InstanceName = "";
            });
            services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(redisConfig));

            return services;
        }
    }
}
