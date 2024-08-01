using Application.Foundation;
using Application.Recipes.Repositories;
using Domain.Recipes.Entities;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RecipeService( IRecipeRepository recipeRepository, IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRcipe( Recipe recipe )
    {
        await _recipeRepository.CreateRecipe( recipe );
        await _unitOfWork.Save();
    }

    public async Task<List<Recipe>> GetAllRecipes( int page )
    {
        return await _recipeRepository.GetAllRecipes( page );
    }

    public async Task<Recipe> GetCurrentUserRecipe( int recipeId )
    {
        return await _recipeRepository.GetCurrentUserRecipe( recipeId );
    }
}
