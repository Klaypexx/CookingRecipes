using Application.Likes.Services;
using Application.ResultObject;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "likes" )]
[ApiController]
[Authorize]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    private int UserId => int.Parse( User.GetUserId() );

    public LikeController( ILikeService likeService )
    {
        _likeService = likeService;
    }

    [HttpPost]
    [Route( "" )]
    public async Task<IActionResult> AddLike( [FromBody] int recipeId )
    {
        Result result = await _likeService.AddLike( UserId, recipeId );

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
        Result result = await _likeService.RemoveLike( UserId, recipeId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }
}
