using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task<List<RecipeTag>> GetTags( List<string> tagNames );
    Task<List<Tag>> GetTagsByIdWithRecipes( List<int> tagsId );
    Task<List<Tag>> GetTagsToDelete( List<Tag> tags, int recipeId );
    void RemoveTags( List<Tag> tags );
}
