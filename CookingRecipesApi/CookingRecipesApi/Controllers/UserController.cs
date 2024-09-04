using Application.Users.Entities;
using CookingRecipesApi.Dto.UsersDto;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CookingRecipesApi.Dto.Extensions;
using Application.Users.Services;
using Application.ResultObject;

namespace CookingRecipesApi.Controllers;

[Route( "users" )]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private string UserName => User.GetUserName();

    public UserController( IUserService userService )
    {
        _userService = userService;
    }

    [HttpPut]
    [Route( "" )]
    public async Task<IActionResult> UpdateUser( [FromForm] UserDto userDto )
    {
        Result result = await _userService.UpdateUser( userDto.ToApplication(), UserName );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }

    [HttpGet]
    [Route( "" )]
    public async Task<IActionResult> GetUser()
    {
        Result<UserInfo> result = await _userService.GetUser( UserName );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        UserInfoDto userDto = result.Value.ToUserInfoDto();

        return Ok( userDto );
    }

    [HttpGet]
    [Route( "username" )]
    public IActionResult GetUsername()
    {
        try
        {
            UserNameDto username = new() { UserName = User.GetUserName() };
            return Ok( username );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "statistic" )]
    public async Task<IActionResult> GetUserStatistic()
    {
        Result<UserStatistic> result = await _userService.GetUserStatistic( UserName );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        UserStatisticDto userStatisticDto = result.Value.ToUserStatisticDto();

        return Ok( userStatisticDto );
    }
}
