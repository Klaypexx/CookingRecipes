using Application.ResultObject;

namespace Application.Favourites.Facade;

public interface IFavouriteRecipeFacade
{
    Task<Result> AddFavouriteRecipe( int userId, int recipeId );
    Task<Result> RemoveFavouriteRecipe( int userId, int recipeId );
}
