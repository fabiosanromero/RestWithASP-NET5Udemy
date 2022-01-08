using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Services.Implementation
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                id = IncrementAndGet(),
                FirstName = "Person Name" +i,
                LastName = "Person Last Name" + i,
                Address = "Some Address" + i,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return System.Threading.Interlocked.Increment(ref count);
        }

        public Person FindById(long id)
        {
            return new Person
            {
                id= IncrementAndGet(),
                FirstName="Fabio",
                LastName="Romero",
                Address="Mauá - SP",
                Gender="Male"                
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}
