using Application.Favourites.Services;
using Application.ResultObject;

namespace Application.Favourites.Facade;

public class FavouriteRecipeFacade : IFavouriteRecipeFacade
{
    private readonly IFavouriteRecipeService _favouriteRecipeService;

    public FavouriteRecipeFacade( IFavouriteRecipeService favouriteRecipeService )
    {
        _favouriteRecipeService = favouriteRecipeService;
    }

    public async Task<Result> AddFavouriteRecipe( int userId, int recipeId )
    {
        try
        {
            await _favouriteRecipeService.AddFavouriteRecipe( userId, recipeId );

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result> RemoveFavouriteRecipe( int userId, int recipeId )
    {
        try
        {
            await _favouriteRecipeService.RemoveFavouriteRecipe( userId, recipeId );

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }
}
