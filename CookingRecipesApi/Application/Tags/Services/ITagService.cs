using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task<List<RecipeTag>> GetTags( List<string>? tagNames );
}
