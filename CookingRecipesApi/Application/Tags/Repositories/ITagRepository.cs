using Domain.Recipes.Entities;

namespace Application.Tags.Repositories;

public interface ITagRepository
{
    Task<List<Tag>> GetAllTagsWithRecipeTags();
    Task<List<RecipeTag>> GetRecipesTagsByTagsNames( List<string> tagNames );
    Task CreateTags( List<Tag> newTags );
    void RemoveTags( List<Tag> tags );
}
