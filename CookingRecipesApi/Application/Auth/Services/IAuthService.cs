using Application.Auth.Entities;
using Domain.Auth.Entities;

namespace Application.Auth.Services;

public interface IAuthService
{
    Task RegisterUser( User user );
    Task<AuthTokenSet> SignIn( string userName, string password, int lifetime );
    Task<AuthTokenSet> Refresh( string cookieRefreshToken, int lifetime );
}
