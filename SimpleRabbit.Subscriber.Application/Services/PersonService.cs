using SimpleRabbit.Subscriber.Domain.Dtos.Person;
using SimpleRabbit.Subscriber.Domain.Interfaces.Services;
using SimpleRabbit.Subscriber.Domain.Entities;
using SimpleRabbit.Subscriber.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleRabbit.Subscriber.Domain.Interfaces.Redis;

namespace SimpleRabbit.Subscriber.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonRedisService _personRedisService;

        public PersonService(IPersonRepository personRepository,IPersonRedisService personRedisService)
        {
            _personRepository=personRepository;
            _personRedisService=personRedisService;
        }

        public async Task<string> AddPerson(CreatePersonDto person)
        {
            string id = Guid.NewGuid().ToString();
            var personToAdd = new Person
            {
                Id = id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
            };

            await _personRepository.Add(personToAdd);
            await _personRedisService.Add(personToAdd);

            return id;
        }

        public async Task<List<Person>> GetPersonsFromSqlDB()
        {
            return await _personRepository.Get();
        }

        public async Task<List<Person>> GetPersonsFromRedis()
        {
            return await _personRedisService.Get();
        }
    }
}
