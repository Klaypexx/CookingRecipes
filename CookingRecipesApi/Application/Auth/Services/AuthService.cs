using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Services;
using Application.Foundation;
using Application.Users.Entities;
using Application.Users.Extensions;
using Application.Users.Repositories;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService( IUserService userService, IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork )
    {
        _userService = userService;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterUser( UserDomain user )
    {
        bool isUniqueUserName = await _userService.IsUniqueUsername( user.UserName );

        if ( !isUniqueUserName )
        {
            throw new ArgumentException( "Логин пользователя должен быть уникальным" );
        }

        string password = _passwordHasher.GeneratePasswordHash( user.Password );
        user.SetPassword( password );
        await _userRepository.AddUser( user );

        await _unitOfWork.Save();
    }

    public async Task<AuthTokenSet> SignIn( string userName, string password, int lifetime )
    {
        UserDomain user = await _userRepository.GetUserByUsername( userName );

        if ( user is null )
        {
            throw new ArgumentException( "Пользователь не найден" );
        }

        bool result = _passwordHasher.VerifyPasswordHash( password, user.Password );

        if ( !result )
        {
            throw new ArgumentException( "Неверный пароль" );
        }

        AuthTokenSet tokens = GetTokens( user );

        await SetToken( user, tokens.RefreshToken, lifetime );

        return tokens;
    }

    public async Task<AuthTokenSet> Refresh( string cookieRefreshToken, int lifetime )
    {
        UserDomain user = await _userRepository.GetUserByRefreshToken( cookieRefreshToken );

        if ( user is null )
        {
            throw new ArgumentException( "Токен обновления не существует" );
        }

        if ( user.RefreshTokenExpiryTime <= DateTime.Now )
        {
            throw new ArgumentException( "Срок действия токена обновления истек" );
        }

        AuthTokenSet tokens = GetTokens( user );

        await SetToken( user, tokens.RefreshToken, lifetime );

        return tokens;
    }

    private async Task SetToken( UserDomain user, string refreshToken, int lifetime )
    {
        user.SetRefreshToken( refreshToken, lifetime );
        await _unitOfWork.Save();
    }

    private AuthTokenSet GetTokens( UserDomain user )
    {
        string jwtToken = _tokenService.GenerateJwtToken( user );
        string refreshToken = _tokenService.GenerateRefreshToken();

        AuthTokenSet tokens = new()
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken,
        };

        return tokens;
    }
}
