using RecipeApplication = Application.Recipes.Entities.Recipe;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Services;
public interface IRecipeService
{
    Task CreateRecipe( RecipeApplication recipe, string rootPath );
    Task RemoveRecipe( int recipeId );
    Task<List<RecipeDomain>> GetRecipesForPage( int skipRange );
    Task<RecipeDomain> GetByIdWithAllDetails( int recipeId );
    Task<RecipeDomain> GetByIdWithTag( int recipeId );
}
