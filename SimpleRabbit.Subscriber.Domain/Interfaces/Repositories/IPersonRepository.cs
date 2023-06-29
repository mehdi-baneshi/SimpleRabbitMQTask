using SimpleRabbit.Subscriber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.Domain.Interfaces.Repositories
{
    public interface IPersonRepository: IGenericRepository<Person>
    {
    }
}
