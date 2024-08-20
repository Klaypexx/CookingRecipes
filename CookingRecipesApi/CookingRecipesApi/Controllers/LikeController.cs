using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "likes" )]
[ApiController]
public class LikeController : ControllerBase
{
    [HttpPost]
    [Route( "" )]
    [Authorize]
    public async Task<IActionResult> AddLike( [FromRoute] int recipeId )
    {
        try
        {
            int userId = int.Parse( User.GetUserId() );

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
    public async Task<IActionResult> RemoveLike( [FromRoute] int recipeId )
    {
        return Ok();
    }

}
