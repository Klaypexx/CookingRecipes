using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task CreateTags( List<Tag> newTags );
    Task<List<Tag>> GetTagsByNames( List<string> tagNames );
    Task<List<Tag>> GetTagsByIdWithRecipes( List<int> tagsId );
    Task RemoveTags( int recipeId, List<int> tagsId );
}
