using Application.Favourites.Services;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "favourites" )]
[ApiController]
public class FavouriteRecipeController : ControllerBase
{
    private readonly IFavouriteRecipeService _favouriteService;

    public FavouriteRecipeController( IFavouriteRecipeService favouriteService )
    {
        _favouriteService = favouriteService;
    }

    [HttpPost]
    [Route( "" )]
    public async Task<IActionResult> AddFavouriteRecipe( [FromQuery] int recipeId )
    {
        try
        {
            int userId = int.Parse( User.GetUserId() );

            await _favouriteService.AddFavouriteRecipe( userId, recipeId );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }

    [HttpDelete]
    [Route( "" )]
    public async Task<IActionResult> RemoveFavouriteRecipe( [FromQuery] int recipeId )
    {
        try
        {
            int userId = int.Parse( User.GetUserId() );

            await _favouriteService.RemoveFavouriteRecipe( userId, recipeId );

            return Ok();
        }
        catch ( Exception exception )
        {
            return BadRequest( new ErrorResponse( exception.Message ) );
        }
    }
}
