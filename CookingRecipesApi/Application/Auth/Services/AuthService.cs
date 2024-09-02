﻿using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Extensions;
using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Foundation;
using UserDomain = Domain.Auth.Entities.User;

namespace Application.Users.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserCreator _userCreator;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService( IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher, IUserCreator userCreator, IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _userCreator = userCreator;
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterUser( UserDomain user )
    {
        bool isUniqueUserName = await IsUniqueUsername( user.UserName );

        if ( !isUniqueUserName )
        {
            throw new ArgumentException( "Логин пользователя должен быть уникальным" );
        }

        string password = _passwordHasher.GeneratePasswordHash( user.Password );
        user.SetPassword( password );
        await _userRepository.AddUser( user );

        await _unitOfWork.Save();
    }

    public async Task<AuthTokenSet> SignIn( string userName, string password, int lifetime )
    {
        UserDomain user = await _userRepository.GetUserByUsername( userName );

        if ( user is null )
        {
            throw new ArgumentException( "Пользователь не найден" );
        }

        bool result = _passwordHasher.VerifyPasswordHash( password, user.Password );

        if ( !result )
        {
            throw new ArgumentException( "Неверный пароль" );
        }

        AuthTokenSet tokens = GetTokens( user );

        await SetToken( user, tokens.RefreshToken, lifetime );

        return tokens;
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

    public async Task<AuthTokenSet> Refresh( string cookieRefreshToken, int lifetime )
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

        return tokens;
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

    private async Task<bool> IsUniqueUsername( string username )
    {
        UserDomain user = await _userRepository.GetUserByUsername( username );

        return user is null;
    }
}
