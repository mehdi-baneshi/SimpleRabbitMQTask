using SimpleRabbit.Core.Domain.Bus;
using SimpleRabbit.Subscriber.Domain.Dtos.Person;
using SimpleRabbit.Subscriber.Application.Events;
using SimpleRabbit.Subscriber.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Subscriber.Application.EventHandlers
{
    public class PersonEventHandler : IEventHandler<PersonCreatedEvent>
    {
        private readonly IPersonService _personService;

        public PersonEventHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public Task Handle(PersonCreatedEvent @event)
        {
            _personService.AddPerson(new CreatePersonDto
            {
                Age = @event.Age,
                FirstName = @event.FirstName,
                LastName = @event.LastName
            });

            return Task.CompletedTask;
        }
    }
}
