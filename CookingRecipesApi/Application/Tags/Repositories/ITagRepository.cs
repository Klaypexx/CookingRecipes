using Domain.Recipes.Entities;

namespace Application.Tags.Repositories;
public interface ITagRepository
{
    Task<List<Tag>> GetTagsByNames( List<string> tagNames );
    Task<List<Tag>> GetTagsByIdWithRecipes( List<int> tagsName );
    Task CreateTags( List<Tag> newTags );
    void RemoveTags( List<Tag> tags );
}
