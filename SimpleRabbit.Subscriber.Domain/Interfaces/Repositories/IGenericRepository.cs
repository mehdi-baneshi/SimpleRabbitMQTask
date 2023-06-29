using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Get();
        Task<T> Add(T entity);
    }
}
