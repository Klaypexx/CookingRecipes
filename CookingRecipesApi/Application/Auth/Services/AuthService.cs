using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Extensions;
using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Foundation;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserCreator _userCreator;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService( IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher, IUserCreator userCreator, IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _userCreator = userCreator;
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterUser( UserDomain user )
    {
        bool isUniqueUserName = await IsUniqueUsername( user.UserName );

        if ( !isUniqueUserName )
        {
            throw new Exception( "Логин пользователя должен быть уникальным" );
        }

        string password = _passwordHasher.GeneratePasswordHash( user.Password );
        user.SetPassword( password );
        await _userRepository.AddUser( user );

        await _unitOfWork.Save();
    }

    public async Task<AuthTokenSet> SignIn( string userName, string password, int lifetime )
    {
        UserDomain user = await _userRepository.GetByUsername( userName );

        if ( user is null )
        {
            throw new Exception( "Пользователь не найден" );
        }

        bool result = _passwordHasher.VerifyPasswordHash( password, user.Password );

        if ( !result )
        {
            throw new Exception( "Неверный пароль" );
        }

        AuthTokenSet tokens = GetTokens( user );

        await SetToken( user, tokens.RefreshToken, lifetime );

        return tokens;
    }

    public async Task UpdateUser( User user, string userName )
    {
        if ( user.UserName != userName )
        {
            bool isUniqueUserName = await IsUniqueUsername( user.UserName );

            if ( !isUniqueUserName )
            {
                throw new Exception( "Логин пользователя должен быть уникальным" );
            }
        }

        UserDomain userDomain = await _userRepository.GetByUsername( userName );

        userDomain.UpdateUser( _userCreator.Create( user ) );
    }

    public async Task<AuthTokenSet> Refresh( string cookieRefreshToken, int lifetime )
    {
        UserDomain user = await _userRepository.GetByRefreshToken( cookieRefreshToken );

        if ( user is null )
        {
            throw new Exception( "Токен обновления не существует" );
        }

        if ( user.RefreshTokenExpiryTime <= DateTime.Now )
        {
            throw new Exception( "Срок действия токена обновления истек" );
        }

        AuthTokenSet tokens = GetTokens( user );

        await SetToken( user, tokens.RefreshToken, lifetime );

        return tokens;
    }

    public async Task<UserInfo> GetUser( string userName )
    {
        UserDomain user = await _userRepository.GetByUsername( userName );

        return user.ToUser();
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

    private async Task<bool> IsUniqueUsername( string username )
    {
        UserDomain user = await _userRepository.GetByUsername( username );

        return user is null;
    }
}
