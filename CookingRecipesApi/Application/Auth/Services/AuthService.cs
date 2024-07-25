using Application.Auth.Entities;
using Application.Auth.Repositories;
using Application.Foundation.Entities;
using Application.Users.Entities;
using Domain.Auth.Entities;

namespace Application.Users.Services;
public class AuthService : IAuthService
{
    private IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private IPasswordHasher _passwordHasher;

    public AuthService( IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
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
        user.Password = _passwordHasher.Generate( user.Password );
        await _userRepository.AddUser( user );
        await _unitOfWork.Save();
    }
}
