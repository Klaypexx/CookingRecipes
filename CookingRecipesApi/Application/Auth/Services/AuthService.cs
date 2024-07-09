using Application.Auth.Repositories;
using Application.Foundation.Entities;
using Application.Users.Entities;
using Domain.Auth.Entities;

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

    public async Task RegisterUser( User user )
    {
        await _userRepository.AddUser( user );
        await _unitOfWork.Save();
    }
}
