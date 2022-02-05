using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserDTO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

            return _context.Users.FirstOrDefault(u=>(u.UserName==user.UserName) && (u.Password==pass));
        }


        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == username));
        }
        public bool RevokeToken(string username)
        {
            var user= _context.Users.SingleOrDefault(u => (u.UserName == username));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }
        private string ComputeHash(string password, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;
            
            var result = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return result;
        }
    }
}
