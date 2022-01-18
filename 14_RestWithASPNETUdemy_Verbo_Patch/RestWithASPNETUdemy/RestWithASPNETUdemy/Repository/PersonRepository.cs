using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository.Generic;
using System;
using System.Linq;

namespace RestWithASPNETUdemy.Repository
{
    public class PersonRepository:GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disable(long Id)
        {
            if (!_context.Persons.Any(p=>p.id.Equals(Id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.id.Equals(Id));
            if(user != null)
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
    }
}
