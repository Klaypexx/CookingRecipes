using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task CreateTags( List<Tag> newTags );
    Task<List<Tag>> GetExistingTagsByName( List<string> tagNames );
    Task<List<Tag>> GetTagsByNameWithRecipes( List<string> tagsName );
    Task RemoveTags( int recipeId, List<string> tagsName );
}
