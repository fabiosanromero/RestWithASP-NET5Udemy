using RestWithASPNETUdemy.Data.DTO;

namespace RestWithASPNETUdemy.Business
{
    public interface ILoginBusiness
    {
        TokenDTO ValidateCredentials(UserDTO user);

        TokenDTO ValidateCredentials(TokenDTO token);
        bool RevokeToken(string username);
    }
}
