using Application.ResultObject;
using Domain.Recipes.Entities;

namespace Application.Favourites.Services;

public interface IFavouriteRecipeService
{
    Task<Result> AddFavouriteRecipe( int userId, int recipeId );
    Task<Result> RemoveFavouriteRecipe( int userId, int recipeId );
}
