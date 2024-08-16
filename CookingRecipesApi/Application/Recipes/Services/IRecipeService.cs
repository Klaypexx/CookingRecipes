using Domain.Recipes.Entities;

namespace Application.Recipes.Services;
public interface IRecipeService
{
    Task CreateRecipe( Entities.Recipe recipe );
    Task UpdateRecipe( Entities.Recipe recipe, int recipeId );
    Task RemoveRecipe( int recipeId );
    Task<List<Recipe>> GetRecipes( int skipRange );
    Task<Recipe> GetByIdWithAllDetails( int recipeId );
    Task<bool> HasAccessToRecipe( int recipeId, int authorId );
}
