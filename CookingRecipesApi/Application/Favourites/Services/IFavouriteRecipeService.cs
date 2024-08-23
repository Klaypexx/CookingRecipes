using Domain.Recipes.Entities;

namespace Application.Favourites.Services;

public interface IFavouriteRecipeService
{
    Task AddFavouriteRecipe( int userId, int recipeId );
    Task RemoveFavouriteRecipe( int userId, int recipeId );
    IReadOnlyList<int> GetRecipesIdsThatUserAddToFavourite( int userId, IReadOnlyList<Recipe> recipes );
    bool HaveFavouriteRecipeConnectionFromUser( int userId, Recipe recipe );
}
