﻿using CookingRecipesApi.Auth;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Utilities;
using Domain.Auth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Application.Auth.Services;
using Infrastructure.Auth.Utils;
using Application.Foundation;
using Application.Auth.Entities;

namespace CookingRecipesApi.Controllers;

[Route( "users" )]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AuthSettings _authSettings;
    private readonly IValidator<RegisterDto> _registerDtoValidator;
    private readonly IValidator<LoginDto> _loginDtoValidator;

    public AuthController( IAuthService authService,
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        AuthSettings authSettings,
        IValidator<RegisterDto> registerDtoValidator,
        IValidator<LoginDto> loginDtoValidator )
    {
        _authService = authService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _authSettings = authSettings;
        _registerDtoValidator = registerDtoValidator;
        _loginDtoValidator = loginDtoValidator;
    }

    [HttpPost]
    [Route( "register" )]
    public async Task<IActionResult> Register( [FromBody] RegisterDto body )
    {
        ValidationResult validationResult = await _registerDtoValidator.ValidateAsync( body );

        if ( !validationResult.IsValid )
        {
            return BadRequest( new ErrorResponse( validationResult.ToDictionary() ) );
        }

        bool isUniqueUserName = await _authService.IsUniqueUsername( body.UserName );

        if ( !isUniqueUserName )
        {
            return BadRequest( new ErrorResponse( "Логин пользователя должен быть уникальным" ) );
        }

        User user = new()
        {
            Name = body.Name,
            UserName = body.UserName,
            Password = body.Password
        };

        try
        {
            await _authService.RegisterUser( user );
            await _unitOfWork.Save();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }

        return Ok();
    }

    [HttpPost]
    [Route( "login" )]
    public async Task<IActionResult> Login( [FromBody] LoginDto body )
    {
        ValidationResult validationResult = await _loginDtoValidator.ValidateAsync( body );

        if ( !validationResult.IsValid )
        {
            return BadRequest( new ErrorResponse( validationResult.ToDictionary() ) );
        }

        User user = await _authService.GetUserByUsername( body.UserName );

        if ( user is null )
        {
            return BadRequest( new ErrorResponse( "Пользователь не найден" ) );
        }

        bool result = PasswordHasher.VerifyPasswordHash( body.Password, user.Password );

        if ( !result )
        {
            return BadRequest( new ErrorResponse( "Неверный пароль" ) );
        }

        try
        {
            Tokens tokens = _authService.SignIn( user, _authSettings.RefreshLifeTime );

            HttpContext.SetRefreshTokenInsideCookie( tokens.RefreshToken, _authSettings.RefreshLifeTime );

            await _unitOfWork.Save();

            return Ok( tokens.JwtToken );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpPost]
    [Route( "refresh" )]
    public async Task<IActionResult> Refresh()
    {
        HttpContext.Request.Cookies.TryGetValue( "refreshToken", out string cookieRefreshToken );
        User user = await _authService.GetUserByToken( cookieRefreshToken );

        if ( user is null )
        {
            return BadRequest( new ErrorResponse( "Токен обновления не существует" ) );
        }

        if ( user.RefreshTokenExpiryTime <= DateTime.Now )
        {
            return BadRequest( new ErrorResponse( "Срок действия токена обновления истек" ) );
        }

        try
        {
            Tokens tokens = _authService.SignIn( user, _authSettings.RefreshLifeTime );

            HttpContext.SetRefreshTokenInsideCookie( tokens.RefreshToken, _authSettings.RefreshLifeTime );

            await _unitOfWork.Save();

            return Ok( tokens.JwtToken );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpPost]
    [Route( "logout" )]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Request.Cookies.TryGetValue( "refreshToken", out string cookieRefreshToken );
        User user = await _authService.GetUserByToken( cookieRefreshToken );

        if ( user == null )
        {
            return BadRequest( new ErrorResponse( "Токен обновления не существует" ) );
        }

        try
        {
            user.SetRefreshToken( "", 0 );

            HttpContext.Response.Cookies.Delete( "refreshToken" );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }


    [HttpGet]
    [Route( "user" )]
    [Authorize]
    public async Task<IActionResult> GetUserByUsername( [FromHeader] string userName )
    {
        try
        {
            User user = await _authService.GetUserByUsername( userName );
            return Ok( user );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "username" )]
    [Authorize]
    public IActionResult GetUsername()
    {
        try
        {
            UserDto username = new() { UserName = User.GetUserName() };
            return Ok( username );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }
}
