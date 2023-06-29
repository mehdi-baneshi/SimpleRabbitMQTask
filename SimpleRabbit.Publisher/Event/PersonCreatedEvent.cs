using SimpleRabbit.Core.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Publisher.Event
{
    public class PersonCreatedEvent:Core.Domain.Event.Event
    {
        public PersonCreatedEvent(string firstName,string lastName, int age)
        {
            FirstName=firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
    }
}
