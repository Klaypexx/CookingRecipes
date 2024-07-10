using Application.Auth.Entities;
using Application.Users.Entities;
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

    public AuthController( IAuthService authService, IPasswordHasher passwordHasher, ITokenService tokenService )
    {
        _authService = authService;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
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

        string token = _tokenService.GenerateJwtToken( user );

        return Results.Ok( token );
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
