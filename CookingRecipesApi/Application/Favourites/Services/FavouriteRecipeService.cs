using Application.Favourites.Repositories;
using Domain.Recipes.Entities;

namespace Application.Favourites.Services;

public class FavouriteRecipeService : IFavouriteRecipeService
{
    private readonly IFavouriteRecipeRepository _favouriteRepository;

    public FavouriteRecipeService( IFavouriteRecipeRepository favouriteRepository )
    {
        _favouriteRepository = favouriteRepository;
    }
    public async Task AddFavouriteRecipe( int userId, int recipeId )
    {
        FavouriteRecipe favouriteRecipeToAdd = new( userId, recipeId );

        await _favouriteRepository.AddFavouriteRecipe( favouriteRecipeToAdd );
    }

    public async Task RemoveFavouriteRecipe( int userId, int recipeId )
    {
        FavouriteRecipe favouriteRecipeToRemove = await _favouriteRepository.GetFavouriteRecipe( userId, recipeId );

        _favouriteRepository.RemoveFavouriteRecipe( favouriteRecipeToRemove );
    }
}
