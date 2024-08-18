using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Services;
using Application.Foundation;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Utilities;
using Domain.Auth.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "users" )]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly AuthSettings _authSettings;
    private readonly IValidator<RegisterDto> _registerDtoValidator;
    private readonly IValidator<LoginDto> _loginDtoValidator;

    public AuthController( IAuthService authService,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        AuthSettings authSettings,
        IValidator<RegisterDto> registerDtoValidator,
        IValidator<LoginDto> loginDtoValidator )
    {
        _authService = authService;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
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

        User user = new( body.Name, body.UserName, body.Password );

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

        bool result = _passwordHasher.VerifyPasswordHash( body.Password, user.Password );

        if ( !result )
        {
            return BadRequest( new ErrorResponse( "Неверный пароль" ) );
        }

        try
        {
            AuthTokenSet tokens = _authService.SignIn( user, _authSettings.RefreshLifeTime );

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
            AuthTokenSet tokens = _authService.SignIn( user, _authSettings.RefreshLifeTime );

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
        try
        {
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
