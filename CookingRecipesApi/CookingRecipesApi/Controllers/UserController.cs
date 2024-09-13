using Application.Users.Entities;
using CookingRecipesApi.Dto.UsersDto;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CookingRecipesApi.Dto.Extensions;
using Application.ResultObject;
using Application.Users.Facade;

namespace CookingRecipesApi.Controllers;

[Route( "users" )]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserFacade _userFacade;

    private string UserName => User.GetUserName();

    public UserController( IUserFacade userFacade )
    {
        _userFacade = userFacade;
    }

    [HttpPut]
    [Route( "" )]
    public async Task<IActionResult> UpdateUser( [FromForm] UserDto userDto )
    {
        Result result = await _userFacade.UpdateUser( userDto.ToApplication(), UserName );

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
        Result<UserInfo> result = await _userFacade.GetUser( UserName );

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
        UserNameDto username = new() { UserName = User.GetUserName() };
        return Ok( username );

    }


    [HttpGet]
    [Route( "name" )]
    public IActionResult GetNameOfUser()
    {
        NameOfUserDto name = new() { Name = User.GetNameOfUser() };
        return Ok( name );
    }

    [HttpGet]
    [Route( "statistic" )]
    public async Task<IActionResult> GetUserStatistic()
    {
        Result<UserStatistic> result = await _userFacade.GetUserStatistic( UserName );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        UserStatisticDto userStatisticDto = result.Value.ToUserStatisticDto();

        return Ok( userStatisticDto );
    }
}
