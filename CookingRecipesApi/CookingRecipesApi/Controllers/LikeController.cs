using Application.Likes.Facade;
using Application.ResultObject;
using CookingRecipesApi.Extensions;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "likes" )]
[ApiController]
[Authorize]
public class LikeController : ControllerBase
{
    private readonly ILikeFacade _likeFacade;

    private int UserId => int.Parse( User.GetUserId() );

    public LikeController( ILikeFacade likeFacade )
    {
        _likeFacade = likeFacade;
    }

    [HttpPost]
    [Route( "" )]
    public async Task<IActionResult> AddLike( [FromBody] int recipeId )
    {
        Result result = await _likeFacade.AddLike( UserId, recipeId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }

    [HttpDelete]
    [Route( "{recipeId}" )]
    public async Task<IActionResult> RemoveLike( [FromRoute] int recipeId )
    {
        Result result = await _likeFacade.RemoveLike( UserId, recipeId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }
}
