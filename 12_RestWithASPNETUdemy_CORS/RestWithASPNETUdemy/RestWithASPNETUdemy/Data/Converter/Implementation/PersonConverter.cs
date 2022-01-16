using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.Converter.Implementation
{
    public class PersonConverter : IParser<PersonDTO, Person>, IParser<Person, PersonDTO>
    {
        public Person Parse(PersonDTO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                id=origin.id,
                FirstName=origin.FirstName,
                LastName=origin.LastName,
                Address=origin.Address,
                Gender=origin.Gender
            };
        }

        public PersonDTO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonDTO
            {
                id = origin.id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<Person> Parse(List<PersonDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonDTO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
