using Application.Favourites.Repositories;
using Application.Foundation;
using Application.ResultObject;
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
    public async Task<Result> AddFavouriteRecipe( int userId, int recipeId )
    {
        try
        {
            FavouriteRecipe favouriteRecipeToAdd = new( userId, recipeId );

            await _favouriteRepository.AddFavouriteRecipe( favouriteRecipeToAdd );

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
            FavouriteRecipe favouriteRecipeToRemove = await _favouriteRepository.GetFavouriteRecipe( userId, recipeId );

            _favouriteRepository.RemoveFavouriteRecipe( favouriteRecipeToRemove );

            await _unitOfWork.Save();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }
}
