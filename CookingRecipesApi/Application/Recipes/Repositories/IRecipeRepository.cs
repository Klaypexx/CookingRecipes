using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;
public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    Task<List<Recipe>> GetAllRecipes( int page );
    Task<Recipe> GetCurrentUserRecipe( int recipeId );
}
