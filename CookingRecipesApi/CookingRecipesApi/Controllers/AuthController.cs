using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Facade;
using Application.ResultObject;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.Extensions;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "auth" )]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthFacade _authFacade;
    private readonly AuthSettings _authSettings;

    public AuthController( IAuthFacade authFacade, AuthSettings authSettings )
    {
        _authFacade = authFacade;
        _authSettings = authSettings;
    }

    [HttpPost]
    [Route( "register" )]
    public async Task<IActionResult> Register( [FromBody] RegisterDto registerDto )
    {
        Result result = await _authFacade.RegisterUser( registerDto.ToRegister() );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }

    [HttpPost]
    [Route( "login" )]
    public async Task<IActionResult> Login( [FromBody] LoginDto loginDto )
    {
        Result<AuthTokenSet> result = await _authFacade.SignIn( loginDto.ToLogin(), _authSettings.RefreshLifeTime );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        HttpContext.SetRefreshTokenInsideCookie( result.Value.RefreshToken, _authSettings.RefreshLifeTime );

        return Ok( result.Value.JwtToken );
    }

    [HttpPost]
    [Route( "isAuth" )]
    [Authorize]
    public IActionResult CheckAuth()
    {
        return Ok();
    }

    [HttpPost]
    [Route( "refresh" )]
    public async Task<IActionResult> Refresh()
    {
        HttpContext.Request.Cookies.TryGetValue( "refreshToken", out string cookieRefreshToken );

        Result<AuthTokenSet> result = await _authFacade.Refresh( cookieRefreshToken, _authSettings.RefreshLifeTime );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        HttpContext.SetRefreshTokenInsideCookie( result.Value.RefreshToken, _authSettings.RefreshLifeTime );

        return Ok( result.Value.JwtToken );
    }

    [HttpPost]
    [Route( "logout" )]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete( "refreshToken" );

        return Ok();
    }
}
