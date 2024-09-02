using Application.Auth;
using Application.Foundation;
using Application.Users.Entities;
using Application.Users.Extensions;
using Application.Users.Repositories;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserCreator _userCreator;
    private readonly IUnitOfWork _unitOfWork;

    public UserService( IUserRepository userRepository, IPasswordHasher passwordHasher, IUserCreator userCreator, IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userCreator = userCreator;
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateUser( User user, string userName )
    {
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
    }

    public async Task<UserInfo> GetUser( string userName )
    {
        UserDomain user = await _userRepository.GetUserByUsername( userName );

        return user.ToUserInfo();
    }

    public async Task<UserStatistic> GetUserStatistic( string userName )
    {
        UserDomain userStatistic = await _userRepository.GetUserByUsernameIncludingDependentEntities( userName );

        return userStatistic.ToUserStatistic();
    }

    public async Task<bool> IsUniqueUsername( string username )
    {
        UserDomain user = await _userRepository.GetUserByUsername( username );

        return user is null;
    }
}
