using Application.Auth.Entities;
using Application.Foundation.Entities;
using Application.Users.Entities;
using CookingRecipesApi.Auth;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Utilities;
using Domain.Auth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;

namespace CookingRecipesApi.Controllers;

[Route( "users" )]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AuthSettings _authSettings;
    private readonly IValidator<RegisterDto> _registerDtoValidator;
    private readonly IValidator<LoginDto> _loginDtoValidator;

    public AuthController( IAuthService authService,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        AuthSettings authSettings,
        IValidator<RegisterDto> registerDtoValidator,
        IValidator<LoginDto> loginDtoValidator )
    {
        _authService = authService;
        _passwordHasher = passwordHasher;
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
            return BadRequest( new { message = validationResult.ToDictionary() } );
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
        }
        catch ( Exception exception )
        {
            return BadRequest( new { message = exception.Message } );
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
            return BadRequest( new { message = validationResult.ToDictionary() } );
        }

        User user = await _authService.GetUserByUsername( body.UserName );

        if ( user is null )
        {
            return BadRequest( new { message = "Пользователь не найден" } );
        }

        bool result = _passwordHasher.Verify( body.Password, user.Password );

        if ( !result )
        {
            return BadRequest( new { message = "Неверный пароль" } );
        }

        string jwtToken = _tokenService.GenerateJwtToken( user );
        string refreshToken = _tokenService.GenerateRefreshToken();
        _tokenService.SetRefreshTokenInsideCookie( refreshToken, HttpContext );
        user.SetRefreshToken( refreshToken, _authSettings.RefreshLifeTime );
        await _unitOfWork.Save();

        /*HttpContext.Response.Cookies.Append( "jwt_token", token );*/
        return Ok( jwtToken );
    }

    [HttpPost]
    [Route( "refresh" )]
    public async Task<IActionResult> Refresh()
    {
        HttpContext.Request.Cookies.TryGetValue( "refreshToken", out string cookieRefreshToken );
        User user = await _authService.GetUserByToken( cookieRefreshToken );

        if ( user is null )
        {
            return BadRequest( "Токен обновления не существует" );
        }

        if ( user.RefreshTokenExpiryTime <= DateTime.UtcNow )
        {
            return BadRequest( "Срок действия токена обновления истек" );
        }

        string jwtToken = _tokenService.GenerateJwtToken( user );
        string refreshToken = _tokenService.GenerateRefreshToken();

        _tokenService.SetRefreshTokenInsideCookie( refreshToken, HttpContext );

        user.SetRefreshToken( refreshToken, _authSettings.RefreshLifeTime );
        await _unitOfWork.Save();

        return Ok( jwtToken );
    }

    [HttpPost]
    [Route( "logout" )]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Request.Cookies.TryGetValue( "refreshToken", out string cookieRefreshToken );
        User user = await _authService.GetUserByToken( cookieRefreshToken );

        if ( user == null )
        {
            return BadRequest( "Токен обновления не существует" );
        }

        user.SetRefreshToken( "", 0 );
        await _unitOfWork.Save();

        HttpContext.Response.Cookies.Delete( "refreshToken" );

        return Ok();
    }


    [HttpGet]
    [Route( "user" )]
    [Authorize]
    public async Task<IActionResult> GetUserByUsername( [FromHeader] string userName )
    {
        User user = await _authService.GetUserByUsername( userName );
        return Ok( user );
    }

    [HttpGet]
    [Route( "username" )]
    [Authorize]
    public IActionResult GetUsername()
    {
        return Ok( new UserDto() { UserName = User.GetUserName() } );
    }
}
