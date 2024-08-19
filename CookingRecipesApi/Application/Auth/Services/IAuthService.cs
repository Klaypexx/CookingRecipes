using Application.Auth.Entities;
using Domain.Auth.Entities;

namespace Application.Auth.Services;

public interface IAuthService
{
    Task RegisterUser( User user );
    Task<User> GetUserByUsername( string username );
    Task<User> GetUserByToken( string token );
    Task<bool> IsUniqueUsername( string username );
    Task<AuthTokenSet> SignIn( User user, int lifetime );
}
