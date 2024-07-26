using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Foundation.Entities;
using Domain.Auth.Entities;
using Infrastructure.Auth.Utils;

namespace Application.Users.Services;
public class AuthService : IAuthService
{
    private IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService( IUserRepository userRepository, IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
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
        await _unitOfWork.Save();
    }
}
