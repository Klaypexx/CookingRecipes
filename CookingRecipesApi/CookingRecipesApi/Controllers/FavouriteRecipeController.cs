using Application.Favourites.Facade;
using Application.ResultObject;
using CookingRecipesApi.Extensions;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "favourites" )]
[ApiController]
public class FavouriteRecipeController : ControllerBase
{
    private readonly IFavouriteRecipeFacade _favouriteRecipeFacade;

    private int AuthorId => int.Parse( User.GetUserId() );

    public FavouriteRecipeController( IFavouriteRecipeFacade favouriteRecipeFacade )
    {
        _favouriteRecipeFacade = favouriteRecipeFacade;
    }

    [HttpPost]
    [Route( "" )]
    public async Task<IActionResult> AddFavouriteRecipe( [FromBody] int recipeId )
    {
        Result result = await _favouriteRecipeFacade.AddFavouriteRecipe( AuthorId, recipeId );

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
        Result result = await _favouriteRecipeFacade.RemoveFavouriteRecipe( AuthorId, recipeId );

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok();
    }
}
