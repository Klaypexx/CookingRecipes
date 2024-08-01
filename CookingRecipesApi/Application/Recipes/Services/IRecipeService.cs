using Domain.Recipes.Entities;

namespace Application.Recipes.Services;
public interface IRecipeService
{
    Task CreateRcipe( Recipe recipe );
    Task<List<Recipe>> GetAllRecipes( int page );
    Task<Recipe> GetCurrentUserRecipe( int recipeId );
}
