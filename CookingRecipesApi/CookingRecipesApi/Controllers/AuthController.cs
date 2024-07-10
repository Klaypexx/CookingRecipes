using Application.Users.Entities;
using CookingRecipesApi.Dto.AuthDto;
using Domain.Auth.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "users" )]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController( IAuthService authService )
    {
        _authService = authService;
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
        string token = await _authService.Login( body.UserName, body.Password );

        return Results.Ok( token );
    }

    [HttpGet]
    [Route( "user" )]
    public async Task<IActionResult> GetUserByUsername( [FromHeader] string userName )
    {
        User user = await _authService.GetUserByUsername( userName );
        return Ok( user );
    }
}
