using Domain.Recipes.Entities;

namespace Application.Favourites.Repositories;

public interface IFavouriteRecipeRepository
{
    Task AddFavouriteRecipe( FavouriteRecipe favouriteRecipe );
    void RemoveFavouriteRecipe( FavouriteRecipe favouriteRecipe );
    Task<FavouriteRecipe> GetFavouriteRecipeConnection( int userId, int recipeId );
}
