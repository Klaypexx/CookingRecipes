using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;
public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    Task<List<Recipe>> GetRecipesForPage( int skipRange );
    Task<Recipe> GetByIdWithAllDetails( int recipeId );
}
