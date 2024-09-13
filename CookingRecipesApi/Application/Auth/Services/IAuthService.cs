using Application.Auth.Entities;

namespace Application.Auth.Services;

public interface IAuthService
{
    Task RegisterUser( Register user );
    Task<AuthTokenSet> SignIn( Login login, int lifetime );
    Task<AuthTokenSet> Refresh( string cookieRefreshToken, int lifetime );
}
