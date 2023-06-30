using Microsoft.Extensions.Caching.Distributed;
using SimpleRabbit.Subscriber.Domain.Entities;
using SimpleRabbit.Subscriber.Domain.Interfaces.Redis;
using SimpleRabbit.Subscriber.Domain.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.RedisDataAccess.RedisService
{
    public class PersonRedisService : RedisService<Person>, IPersonRedisService
    {
        public PersonRedisService(IDistributedCache cache, IConnectionMultiplexer redis) : base(cache, redis)
        {
        }
    }
}
