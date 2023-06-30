using SimpleRabbit.Subscriber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.Domain.Interfaces.Redis
{
    public interface IRedisService<T> where T : BaseEntity
    {
        Task<List<T>> Get();
        Task Add(T entity);
    }
}
