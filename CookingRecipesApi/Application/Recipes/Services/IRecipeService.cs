using Application.Recipes.Entities;

namespace Application.Recipes.Services;

public interface IRecipeService
{
    Task CreateRecipe( Recipe recipe );
    Task UpdateRecipe( Recipe recipe, int recipeId );
    Task RemoveRecipe( int recipeId );
    Task<IReadOnlyList<OverviewRecipe>> GetRecipes( int pageNumber );
    Task<CompleteRecipe> GetRecipeById( int recipeId );
    Task<bool> HasAccessToRecipe( int recipeId, int authorId );
}
