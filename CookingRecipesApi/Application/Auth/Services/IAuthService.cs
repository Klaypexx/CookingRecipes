using Application.Auth.Entities;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Auth.Services;

public interface IAuthService
{
    Task RegisterUser( UserDomain user );
    Task<AuthTokenSet> SignIn( string userName, string password, int lifetime );
    Task<AuthTokenSet> Refresh( string cookieRefreshToken, int lifetime );
}
