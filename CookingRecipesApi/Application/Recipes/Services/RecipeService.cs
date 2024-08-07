using Application.Foundation;
using Application.Recipes.Repositories;
using Domain.Recipes.Entities;

namespace Application.Recipes.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    public RecipeService( IRecipeRepository recipeRepository )
    {
        _recipeRepository = recipeRepository;
    }

    public async Task CreateRcipe( Recipe recipe )
    {
        await _recipeRepository.CreateRecipe( recipe );
    }

    public async Task<List<Recipe>> GetRecipesForPage( int skipRange )
    {
        return await _recipeRepository.GetRecipesForPage( skipRange );
    }

    public async Task<Recipe> GetByIdWithAllDetails( int recipeId )
    {
        return await _recipeRepository.GetByIdWithAllDetails( recipeId );
    }
}
