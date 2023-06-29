using Microsoft.Extensions.Configuration;
using SimpleRabbit.Core.Domain.Bus;
using SimpleRabbit.Publisher.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRabbit.Publisher
{
    public class App
    {
        private readonly IEventBus _bus;
        private readonly List<Person> _persons;

        public App(IEventBus bus)
        {
            _bus = bus;
            _persons = new List<Person>();
        }

        public void Run(string[] args)
        {
            GetSamplePersons();

            Task.Run(async delegate
            {
                await PublishWithDely();
            }).Wait();

            Console.WriteLine("finished");
            Console.ReadLine();
        }

        private void GetSamplePersons()
        {
            var person1 = new Person("Ahmad", "Majlesara", 35);
            var person2 = new Person("Borna", "Shayesteh", 30);
            var person3 = new Person("Ghasem", "Mokhaberati", 50);
            var person4 = new Person("Panjali", "Shayegan", 90);
            var person5 = new Person("Pouya", "Jalilvand", 25);

            _persons.Add(person1);
            _persons.Add(person2);
            _persons.Add(person3);
            _persons.Add(person4);
            _persons.Add(person5);
        }

        public async Task PublishWithDely()
        {
            foreach (var person in _persons)
            {
                _bus.Publish(new PersonCreatedEvent(person.FirstName, person.LastName, person.Age));
                Console.WriteLine("a message was sent");
                await Task.Delay(10000);
            }
        }
    }

    public class Person
    {
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
    }
}
