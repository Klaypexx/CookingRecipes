using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Services;
public interface IRecipeService
{
    Task CreateRecipe( RecipeApplication recipe, string rootPath );
    Task UpdateRecipe( RecipeApplication recipe, int recipeId, string rootPath );
    Task RemoveRecipe( int recipeId, string rootPath );
    Task<List<RecipeDomain>> GetRecipesForPage( int skipRange );
    Task<RecipeDomain> GetByIdWithAllDetails( int recipeId );
    Task<RecipeDomain> GetByIdWithTag( int recipeId );
    Task<RecipeDomain> GetById( int recipeId );
    Task<bool> HasAccessToRecipe( int recipeId, int authorId );
}
