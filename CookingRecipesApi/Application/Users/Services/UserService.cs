using Application.Auth;
using Application.Foundation;
using Application.ResultObject;
using Application.Users.Entities;
using Application.Users.Extensions;
using Application.Users.Repositories;
using Application.Validation;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserCreator _userCreator;
    private readonly IValidator<User> _userValidator;
    private readonly IUnitOfWork _unitOfWork;

    public UserService( IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUserCreator userCreator,
        IValidator<User> userValidator,
        IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userCreator = userCreator;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> UpdateUser( User user, string userName )
    {
        try
        {
            Result result = _userValidator.Validate( user );

            if ( !result.IsSuccess )
            {
                return result;
            }

            if ( user.UserName != userName )
            {
                bool isUniqueUserName = await IsUniqueUsername( user.UserName );

                if ( !isUniqueUserName )
                {
                    throw new ArgumentException( "Логин пользователя должен быть уникальным" );
                }
            }

            UserDomain userDomain = await _userRepository.GetUserByUsername( userName );

            string hashedPassword = string.Empty;
            if ( !string.IsNullOrEmpty( user.Password ) )
            {
                hashedPassword = _passwordHasher.GeneratePasswordHash( user.Password );
            }

            userDomain.UpdateUser( _userCreator.Create( user, hashedPassword ) );

            await _unitOfWork.Save();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result<UserInfo>> GetUser( string userName )
    {
        try
        {
            UserDomain user = await _userRepository.GetUserByUsername( userName );

            return new Result<UserInfo>( user.ToUserInfo() );
        }
        catch ( Exception e )
        {
            return new Result<UserInfo>( new Error( e.Message ) );
        }
    }

    public async Task<Result<UserStatistic>> GetUserStatistic( string userName )
    {
        try
        {
            UserDomain userStatistic = await _userRepository.GetUserByUsernameWithDetails( userName );

            return new Result<UserStatistic>( userStatistic.ToUserStatistic() );
        }
        catch ( Exception e )
        {
            return new Result<UserStatistic>( new Error( e.Message ) );
        }
    }

    public async Task<bool> IsUniqueUsername( string username )
    {
        UserDomain user = await _userRepository.GetUserByUsername( username );

        return user is null;
    }
}
