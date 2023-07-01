using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using SimpleRabbit.Subscriber.Domain.Entities;
using SimpleRabbit.Subscriber.Domain.Interfaces.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.RedisDataAccess.RedisService
{
    public class RedisService<T>: IRedisService<T> where T : BaseEntity
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
        private readonly string entityName;
        private readonly string host;
        private readonly int port;
        public RedisService(IDistributedCache cache, IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _cache = cache;
            _redis = redis;
            entityName = typeof(T).Name;
            var redisConfig = configuration.GetValue<string>("RedisConnection");
            var splitted = redisConfig.Split(':');
            host= splitted[0];
            port = Int32.Parse(splitted[1]);
        }

        public async Task Add(T entity)
        {
            var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(entity));

            await _cache.SetAsync(entityName+"_" + entity.Id, content, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(1) });
        }

        public async Task<List<T>> Get()
        {
            var redisKeys = _redis.GetServer(host, port).Keys(pattern: $"{entityName}_*")
                .AsQueryable().Select(p => p.ToString()).ToList();

            var items = new List<T>();

            foreach (var redisKey in redisKeys)
            {
                items.Add(JsonSerializer.Deserialize<T>(await _cache.GetStringAsync(redisKey)));
            }

            return items;
        }
    }
}
