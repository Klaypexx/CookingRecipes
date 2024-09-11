using Application.Auth.Entities;
using Application.ResultObject;

namespace Application.Auth.Facade;

public interface IAuthFacade
{
    Task<Result> RegisterUser( Register user );
    Task<Result<AuthTokenSet>> SignIn( Login login, int lifetime );
    Task<Result<AuthTokenSet>> Refresh( string cookieRefreshToken, int lifetime );
}
