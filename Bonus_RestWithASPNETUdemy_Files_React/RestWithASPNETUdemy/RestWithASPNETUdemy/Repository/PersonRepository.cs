using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disable(long Id)
        {
            if (!_context.Persons.Any(p => p.id.Equals(Id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.id.Equals(Id));
            if (user != null)
            {
                try
                {
                    user.Enabled = false;
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(
                    p => p.FirstName.Contains(firstName)
                    && p.LastName.Contains(lastName)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(
                    p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(
                    p => p.FirstName.Contains(firstName)).ToList();
            }
            return null;
        }
    }
}