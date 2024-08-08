using Domain.Recipes.Entities;

namespace Application.Tags.Repositories;
public interface ITagRepository
{
    Task<List<Tag>> GetExistingTags( List<string> tagNames );
    Task<List<Tag>> GetTagsByIdWithRecipes( List<int> tagsId );
    Task<List<Tag>> GetTagsToDelete( List<Tag> tags, int recipeId );
    Task CreateTags( List<Tag> newTags );
    void RemoveTags( List<Tag> tags );
}
