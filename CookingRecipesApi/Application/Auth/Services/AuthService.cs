using Application.Auth.Entities;
using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Auth.Utils;
using Domain.Auth.Entities;

namespace Application.Users.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService( IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher )
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
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
        string password = _passwordHasher.GeneratePasswordHash( user.Password );
        user.SetPassword( password );
        await _userRepository.AddUser( user );
    }

    public async Task<bool> IsUniqueUsername( string username )
    {
        User user = await _userRepository.GetByUsername( username );

        return user is null;
    }

    public Tokens SignIn( User user, int lifetime )
    {
        string jwtToken = _tokenService.GenerateJwtToken( user );
        string refreshToken = _tokenService.GenerateRefreshToken();
        user.SetRefreshToken( refreshToken, lifetime );

        Tokens tokens = new()
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken,
        };

        return tokens;
    }
}
