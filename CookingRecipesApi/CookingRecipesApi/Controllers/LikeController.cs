using Application.Likes.Services;
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

    public LikeController( ILikeService likeService )
    {
        _likeService = likeService;
    }

    [HttpPost]
    [Route( "" )]
    public async Task<IActionResult> AddLike( [FromBody] int recipeId )
    {
        try
        {
            int userId = int.Parse( User.GetUserId() );

            await _likeService.AddLike( userId, recipeId );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpDelete]
    [Route( "{recipeId}" )]
    public async Task<IActionResult> RemoveLike( [FromRoute] int recipeId )
    {
        try
        {
            int userId = int.Parse( User.GetUserId() );

            await _likeService.RemoveLike( userId, recipeId );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

}
