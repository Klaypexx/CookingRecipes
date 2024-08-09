using Domain.Recipes.Entities;

namespace Application.Tags.Repositories;
public interface ITagRepository
{
    Task<List<Tag>> GetExistingTagsByName( List<string> tagNames );
    Task<List<Tag>> GetTagsByNameWithRecipes( List<string> tagsName );
    Task CreateTags( List<Tag> newTags );
    void RemoveTags( List<Tag> tags );
}
