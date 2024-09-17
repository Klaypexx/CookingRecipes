using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Services;
using Application.Users.Repositories;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserCreator _userCreator;

    public AuthService( IUserService userService,
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher,
        IUserCreator userCreator )
    {
        _userService = userService;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _userCreator = userCreator;
    }

    public async Task RegisterUser( Register register )
    {
        bool isUniqueUserName = await _userService.IsUniqueUsername( register.UserName );

        if ( !isUniqueUserName )
        {
            throw new ArgumentException( "Логин пользователя должен быть уникальным" );
        }

        string hashedPassword = _passwordHasher.GeneratePasswordHash( register.Password );

        await _userRepository.AddUser( _userCreator.Create( register, hashedPassword ) );
    }

    public async Task<AuthTokenSet> SignIn( Login login, int lifeTime )
    {
        UserDomain user = await _userRepository.GetUserByUsername( login.UserName );

        if ( user is null )
        {
            throw new ArgumentException( "Неверное имя пользователя или пароль" );
        }

        bool hashResult = _passwordHasher.VerifyPasswordHash( login.Password, user.Password );

        if ( !hashResult )
        {
            throw new ArgumentException( "Неверное имя пользователя или пароль" );
        }

        AuthTokenSet tokens = GetTokens( user );

        user.SetRefreshToken( tokens.RefreshToken, lifeTime );

        return tokens;

    }

    public async Task<AuthTokenSet> Refresh( string cookieRefreshToken, int lifeTime )
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

        user.SetRefreshToken( tokens.RefreshToken, lifeTime );

        return tokens;
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
