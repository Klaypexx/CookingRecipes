using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task<List<RecipeTag>> GetOrCreateTag( List<string> tagNames );
}
