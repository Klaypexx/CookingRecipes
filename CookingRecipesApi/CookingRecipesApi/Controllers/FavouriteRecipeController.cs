using Application.Favourites.Services;
using Application.ResultObject;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "favourites" )]
[ApiController]
public class FavouriteRecipeController : ControllerBase
{
    private readonly IFavouriteRecipeService _favouriteService;

    private int AuthorId => int.Parse( User.GetUserId() );

    public FavouriteRecipeController( IFavouriteRecipeService favouriteService )
    {
        _favouriteService = favouriteService;
    }

    [HttpPost]
    [Route( "" )]
    public async Task<IActionResult> AddFavouriteRecipe( [FromBody] int recipeId )
    {
        Result result = await _favouriteService.AddFavouriteRecipe( AuthorId, recipeId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }

    [HttpDelete]
    [Route( "{recipeId}" )]
    public async Task<IActionResult> RemoveFavouriteRecipe( [FromRoute] int recipeId )
    {
        Result result = await _favouriteService.RemoveFavouriteRecipe( AuthorId, recipeId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }
}
