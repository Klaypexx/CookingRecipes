using Domain.Auth.Entities;

namespace Application.Users.Entities;
public interface IAuthService
{
    Task RegisterUser( User user );
    Task<User> GetUserByUsername( string username );
    Task<User> GetUserByToken( string token );
}
