using Domain.Recipes.Entities;

namespace Application.Tags.Repositories;
public interface ITagRepository
{
    Task<List<Tag>> GetExistingTags( List<string> tagNames );
    Task CreateTags( List<Tag> newTags );
    Task<List<Tag>> GetTagsToDelete( List<int> tagsId, int recipeId );
    void RemoveTags( List<Tag> tags );
}
