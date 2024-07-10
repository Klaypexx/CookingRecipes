using Domain.Auth.Entities;

namespace Application.Users.Entities;
public interface IAuthService
{
    Task RegisterUser( User user );
    Task<string> Login( string userName, string password );
    Task<User> GetUserByUsername( string username );
}
