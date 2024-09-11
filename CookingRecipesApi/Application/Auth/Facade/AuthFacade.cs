using Application.Auth.Entities;
using Application.Auth.Services;
using Application.Foundation;
using Application.ResultObject;
using Application.Users.Repositories;
using Application.Users.Services;
using Application.Users;
using Application.Validation;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Auth.Facade;

public class AuthFacade
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserCreator _userCreator;
    private readonly IValidator<Register> _registerValidator;
    private readonly IValidator<Login> _loginValidator;
    private readonly IUnitOfWork _unitOfWork;

    public AuthFacade( IUserService userService,
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher,
        IUserCreator userCreator,
        IValidator<Register> registerValidator,
        IValidator<Login> loginValidator,
        IUnitOfWork unitOfWork )
    {
        _userService = userService;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _userCreator = userCreator;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> RegisterUser( Register register )
    {
        try
        {
            Result result = _registerValidator.Validate( register );

            if ( !result.IsSuccess )
            {
                return result;
            }

            bool isUniqueUserName = await _userService.IsUniqueUsername( register.UserName );

            if ( !isUniqueUserName )
            {
                throw new ArgumentException( "Логин пользователя должен быть уникальным" );
            }

            string hashedPassword = _passwordHasher.GeneratePasswordHash( register.Password );

            await _userRepository.AddUser( _userCreator.Create( register, hashedPassword ) );

            await _unitOfWork.Save();

            return new Result();

        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result<AuthTokenSet>> SignIn( Login login, int lifeTime )
    {
        try
        {
            Result result = _loginValidator.Validate( login );

            if ( !result.IsSuccess )
            {
                return new Result<AuthTokenSet>( result.Errors );
            }

            UserDomain user = await _userRepository.GetUserByUsername( login.UserName );

            if ( user is null )
            {
                throw new ArgumentException( "Пользователь не найден" );
            }

            bool hashResult = _passwordHasher.VerifyPasswordHash( login.Password, user.Password );

            if ( !hashResult )
            {
                throw new ArgumentException( "Неверный пароль" );
            }

            AuthTokenSet tokens = GetTokens( user );

            await SetToken( user, tokens.RefreshToken, lifeTime );

            return new Result<AuthTokenSet>( tokens );
        }
        catch ( Exception e )
        {
            return new Result<AuthTokenSet>( new Error( e.Message ) );
        }
    }

    public async Task<Result<AuthTokenSet>> Refresh( string cookieRefreshToken, int lifetime )
    {
        try
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

            return new Result<AuthTokenSet>( tokens );
        }
        catch ( Exception e )
        {
            return new Result<AuthTokenSet>( new Error( e.Message ) );
        }
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
