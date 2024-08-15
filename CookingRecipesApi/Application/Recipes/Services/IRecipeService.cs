using Domain.Recipes.Entities;

namespace Application.Recipes.Services;
public interface IRecipeService
{
    Task CreateRecipe( Entities.Recipe recipe, string rootPath );
    Task UpdateRecipe( Entities.Recipe recipe, int recipeId, string rootPath );
    Task RemoveRecipe( int recipeId, string rootPath );
    Task<List<Recipe>> GetRecipesForPage( int skipRange );
    Task<Recipe> GetByIdWithAllDetails( int recipeId );
    Task<bool> HasAccessToRecipe( int recipeId, int authorId );
}
