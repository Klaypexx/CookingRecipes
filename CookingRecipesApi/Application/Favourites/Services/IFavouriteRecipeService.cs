namespace Application.Favourites.Services;

public interface IFavouriteRecipeService
{
    Task AddFavouriteRecipe( int userId, int recipeId );
    Task RemoveFavouriteRecipe( int userId, int recipeId );
}
