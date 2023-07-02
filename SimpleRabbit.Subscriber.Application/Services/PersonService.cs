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
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using SimpleRabbit.Subscriber.Application.Validators;

namespace SimpleRabbit.Subscriber.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonRedisService _personRedisService;
        private readonly IValidator<CreatePersonDto> _validator;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IPersonRepository personRepository, IPersonRedisService personRedisService, IValidator<CreatePersonDto> validator, ILogger<PersonService> logger)
        {
            _personRepository = personRepository;
            _personRedisService = personRedisService;
            _validator = validator;
            _logger = logger;
        }

        public async Task<string> AddPerson(CreatePersonDto person)
        {
            var validationResult =await _validator.ValidateAsync(person);

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            string id = Guid.NewGuid().ToString();
            var personToAdd = new Person
            {
                Id = id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
            };

            bool success=await _personRepository.Add(personToAdd);
            if (!success)
            {
                _logger.LogError($"Something went wrong with adding the person {personToAdd.FirstName} {personToAdd.LastName} to database.");
                throw new Exception($"Something went wrong with adding the person {personToAdd.FirstName} {personToAdd.LastName} to database.");
            }

            await _personRedisService.Add(personToAdd);

            _logger.LogInformation($"Person {personToAdd.FirstName} {personToAdd.LastName} INSERTED to databases.");

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
