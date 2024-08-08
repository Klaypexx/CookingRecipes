using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task<List<RecipeTag>> GetTags( List<string> tagNames );
    Task<List<Tag>> GetTagsToDelete( List<int> tagsId, int recipeId );
    void RemoveTags( List<Tag> tags );
}
