using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Services;
using Application.Foundation;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Utilities;
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

        try
        {
            await _authService.RegisterUser( new( body.Name, body.UserName, body.Password ) );
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

        try
        {
            AuthTokenSet tokens = await _authService.SignIn( body.UserName, body.Password, _authSettings.RefreshLifeTime );

            HttpContext.SetRefreshTokenInsideCookie( tokens.RefreshToken, _authSettings.RefreshLifeTime );

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
        try
        {
            HttpContext.Request.Cookies.TryGetValue( "refreshToken", out string cookieRefreshToken );

            AuthTokenSet tokens = await _authService.Refresh( cookieRefreshToken, _authSettings.RefreshLifeTime );

            HttpContext.SetRefreshTokenInsideCookie( tokens.RefreshToken, _authSettings.RefreshLifeTime );

            return Ok( tokens.JwtToken );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpPost]
    [Route( "logout" )]
    public IActionResult Logout()
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
