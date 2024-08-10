using Domain.Recipes.Entities;

namespace Application.RecipesTags.Services;
public interface IRecipeTagService
{
    Task<List<RecipeTag>> GetRecipeTagsByRecipeIdAndTagIds( int recipeId, List<int> tagId );
    Task RemoveConnections( int recipeId, List<int> tagsId );
    Task CreateConnections( int recipeId, List<int> tagsId );
}
