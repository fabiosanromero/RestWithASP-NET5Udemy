using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
