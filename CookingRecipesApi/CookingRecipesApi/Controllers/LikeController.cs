using Application.Likes.Services;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "likes" )]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikeController( ILikeService likeService )
    {
        _likeService = likeService;
    }

    [HttpPost]
    [Route( "" )]
    [Authorize]
    public async Task<IActionResult> AddLike( [FromQuery] int recipeId )
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
    [Route( "" )]
    [Authorize]
    public async Task<IActionResult> RemoveLike( [FromQuery] int recipeId )
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
