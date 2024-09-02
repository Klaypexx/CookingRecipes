using Application.Users.Entities;
using CookingRecipesApi.Dto.UsersDto;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using CookingRecipesApi.Dto.Extensions;
using Application.Users.Services;

namespace CookingRecipesApi.Controllers;

[Route( "users" )]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<UserDto> _userDtoValidator;

    public UserController( IUserService userService, IValidator<UserDto> userDtoValidator )
    {
        _userService = userService;
        _userDtoValidator = userDtoValidator;
    }

    [HttpPut]
    [Route( "" )]
    public async Task<IActionResult> UpdateUser( [FromForm] UserDto userDto )
    {
        ValidationResult validationResult = await _userDtoValidator.ValidateAsync( userDto );

        if ( !validationResult.IsValid )
        {
            return BadRequest( new ErrorResponse( validationResult.ToDictionary() ) );
        }

        try
        {
            string userName = User.GetUserName();

            await _userService.UpdateUser( userDto.ToApplication(), userName );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpGet]
    [Route( "" )]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            string userName = User.GetUserName();

            UserInfo user = await _userService.GetUser( userName );
            UserInfoDto userDto = user.ToUserInfoDto();

            return Ok( userDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
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
        try
        {
            string userName = User.GetUserName();

            UserStatistic userStatistic = await _userService.GetUserStatistic( userName );
            UserStatisticDto userStatisticDto = userStatistic.ToUserStatisticDto();

            return Ok( userStatisticDto );
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }
}
