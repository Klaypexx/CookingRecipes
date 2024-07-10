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
    private readonly ITokenProvider _tokenService;

    public AuthService( IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenProvider tokenService )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<User> GetUserByUsername( string username )
    {
        return await _userRepository.GetByUsername( username );
    }

    public async Task RegisterUser( User user )
    {
        user.Password = _passwordHasher.Generate( user.Password );
        user.Id = Guid.NewGuid().ToString();
        await _userRepository.AddUser( user );
        await _unitOfWork.Save();
    }

    public async Task<string> Login( string userName, string password )
    {
        User user = await _userRepository.GetByUsername( userName );
        bool result = _passwordHasher.Verify( password, user.Password );

        if ( !result )
        {
            throw new Exception( "Failed to login" );
        }

        string token = _tokenService.GenerateJwtToken( user );

        return token;
    }
}
