using SimpleRabbit.Subscriber.Domain.Dtos.Person;
using SimpleRabbit.Subscriber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task<List<Person>> GetPersons();
        Task<string> AddPerson(CreatePersonDto person);
    }
}
