using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;
public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    Task<List<Recipe>> GetAllUserRecipes( int userId );
}
