using Application.Favourites.Services;
using Application.Foundation;
using Application.ResultObject;

namespace Application.Favourites.Facade;

public class FavouriteRecipeFacade : IFavouriteRecipeFacade
{
    private readonly IFavouriteRecipeService _favouriteRecipeService;
    private readonly IUnitOfWork _unitOfWork;

    public FavouriteRecipeFacade( IFavouriteRecipeService favouriteRecipeService, IUnitOfWork unitOfWork )
    {
        _favouriteRecipeService = favouriteRecipeService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> AddFavouriteRecipe( int userId, int recipeId )
    {
        try
        {
            await _favouriteRecipeService.AddFavouriteRecipe( userId, recipeId );

            await _unitOfWork.Save();

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

            await _unitOfWork.Save();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }
}
