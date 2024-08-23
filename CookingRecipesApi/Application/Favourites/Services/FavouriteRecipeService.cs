using Application.Favourites.Repositories;
using Application.Foundation;
using Domain.Recipes.Entities;

namespace Application.Favourites.Services;

public class FavouriteRecipeService : IFavouriteRecipeService
{
    private readonly IFavouriteRecipeRepository _favouriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FavouriteRecipeService( IFavouriteRecipeRepository favouriteRepository, IUnitOfWork unitOfWork )
    {
        _favouriteRepository = favouriteRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task AddFavouriteRecipe( int userId, int recipeId )
    {
        FavouriteRecipe favouriteRecipeToAdd = new( userId, recipeId );

        await _favouriteRepository.AddFavouriteRecipe( favouriteRecipeToAdd );

        await _unitOfWork.Save();
    }

    public async Task RemoveFavouriteRecipe( int userId, int recipeId )
    {
        FavouriteRecipe favouriteRecipeToRemove = await _favouriteRepository.GetFavouriteRecipeConnection( userId, recipeId );

        _favouriteRepository.RemoveFavouriteRecipe( favouriteRecipeToRemove );

        await _unitOfWork.Save();
    }

    public IReadOnlyList<int> GetRecipesIdsThatUserAddToFavourite( int userId, IReadOnlyList<Recipe> recipes )
    {
        return recipes
                .Where( recipe => recipe.FavouriteRecipes.Any( favouriteRecipe => favouriteRecipe.UserId == userId ) )
                .Select( recipe => recipe.Id )
                .ToList();
    }

    public bool HaveFavouriteRecipeConnectionFromUser( int userId, Recipe recipe )
    {
        return recipe.FavouriteRecipes.Any( favouriteRecipe => favouriteRecipe.UserId == userId );
    }
}
