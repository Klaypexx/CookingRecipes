using System.ComponentModel.DataAnnotations;
using Application.Auth.Entities;
using Application.Foundation.Entities;
using Application.Users.Entities;
using CookingRecipesApi.Auth;
using CookingRecipesApi.Dto.AuthDto;
using Domain.Auth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    public AuthController( IAuthService authService, IPasswordHasher passwordHasher, ITokenService tokenService, IUnitOfWork unitOfWork, AuthSettings authSettings )
    {
        _authService = authService;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _authSettings = authSettings;
    }

    [HttpPost]
    [Route( "register" )]
    public async Task<IActionResult> Register( [FromBody] RegisterDto body )
    {
        User user = new()
        {
            Name = body.Name,
            UserName = body.UserName,
            Password = body.Password
        };
        await _authService.RegisterUser( user );
        return Ok();
    }

    [HttpPost]
    [Route( "login" )]
    public async Task<IResult> Login( [FromBody] LoginDto body )
    {
        User user = await _authService.GetUserByUsername( body.UserName );
        bool result = _passwordHasher.Verify( body.Password, user.Password );

        if ( !result )
        {
            throw new Exception( "Failed to login" );
        }

        string jwtToken = _tokenService.GenerateJwtToken( user );
        string refreshToken = _tokenService.GenerateRefreshToken();
        _tokenService.SetRefreshTokenInsideCookie( refreshToken, HttpContext );
        user.SetRefreshToken( refreshToken, _authSettings.RefreshLifeTime );
        await _unitOfWork.Save();

        /*HttpContext.Response.Cookies.Append( "jwt_token", token );*/

        TokenDto response = new()
        {
            AccessToken = jwtToken
        };
        return Results.Ok( response );
    }

    [HttpPost]
    [Route( "refresh" )]
    public async Task<IActionResult> Refresh()
    {
        HttpContext.Request.Cookies.TryGetValue( "refreshToken", out string cookieRefreshToken );
        User user = await _authService.GetUserByToken( cookieRefreshToken );

        if ( user is null )
        {
            return BadRequest( new Exception( "Токен обновления не существует" ) );
        }

        if ( user.RefreshTokenExpiryTime <= DateTime.UtcNow )
        {
            return BadRequest( new Exception( "Срок действия токена обновления истек" ) );
        }

        string jwtToken = _tokenService.GenerateJwtToken( user );
        string refreshToken = _tokenService.GenerateRefreshToken();

        _tokenService.SetRefreshTokenInsideCookie( refreshToken, HttpContext );

        user.SetRefreshToken( refreshToken, _authSettings.RefreshLifeTime );
        await _unitOfWork.Save();

        TokenDto response = new()
        {
            AccessToken = jwtToken
        };

        return Ok( response );
    }

    [HttpGet]
    [Route( "user" )]
    [Authorize]
    public async Task<IActionResult> GetUserByUsername( [FromHeader] string userName )
    {
        User user = await _authService.GetUserByUsername( userName );
        return Ok( user );
    }
}
