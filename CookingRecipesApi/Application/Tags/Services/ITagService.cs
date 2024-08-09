using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task<List<RecipeTag>> GetTags( List<string> tagNames );
    Task<List<Tag>> GetTagsByNameWithRecipes( List<string> tagsName );
    Task RemoveTags( int recipeId, List<string> tagsName );
}
