using SimpleRabbit.Subscriber.Domain.Dtos.Person;
using SimpleRabbit.Subscriber.Domain.Interfaces.Services;
using SimpleRabbit.Subscriber.Domain.Entities;
using SimpleRabbit.Subscriber.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository=personRepository;
        }

        public async Task<string> AddPerson(CreatePersonDto person)
        {
            string id = Guid.NewGuid().ToString();
            await _personRepository.Add(new Person
            {
                Id =id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
            });

            return id;
        }

        public async Task<List<Person>> GetPersons()
        {
            return await _personRepository.Get();
        }
    }
}
