using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Foundation;
using Domain.Auth.Entities;
using Infrastructure.Auth.Utils;

namespace Application.Users.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService( IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByUsername( string username )
    {
        return await _userRepository.GetByUsername( username );
    }

    public async Task<User> GetUserByToken( string token )
    {
        return await _userRepository.GetByRefreshToken( token );
    }

    public async Task RegisterUser( User user )
    {
        user.Password = PasswordHasher.GeneratePasswordHash( user.Password );
        await _userRepository.AddUser( user );
    }

    public async Task<bool> IsUniqueUsername( string username )
    {
        User user = await _userRepository.GetByUsername( username );

        return user is null;
    }
}
