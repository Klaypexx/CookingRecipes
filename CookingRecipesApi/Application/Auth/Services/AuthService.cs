using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Foundation;
using Domain.Auth.Entities;

namespace Application.Users.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService( IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
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
        string password = _passwordHasher.GeneratePasswordHash( user.Password );
        user.SetPassword( password );
        await _userRepository.AddUser( user );

        await _unitOfWork.Save();
    }

    public async Task<bool> IsUniqueUsername( string username )
    {
        User user = await _userRepository.GetByUsername( username );

        return user is null;
    }

    public async Task<AuthTokenSet> SignIn( User user, int lifetime )
    {
        string jwtToken = _tokenService.GenerateJwtToken( user );
        string refreshToken = _tokenService.GenerateRefreshToken();
        user.SetRefreshToken( refreshToken, lifetime );

        AuthTokenSet tokens = new()
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken,
        };

        await _unitOfWork.Save();

        return tokens;
    }
}
