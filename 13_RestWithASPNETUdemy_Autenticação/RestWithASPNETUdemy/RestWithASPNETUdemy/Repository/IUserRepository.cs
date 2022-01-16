using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserDTO user);
    }
}
