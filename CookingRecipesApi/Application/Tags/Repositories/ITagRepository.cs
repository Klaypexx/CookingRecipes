using Domain.Recipes.Entities;

namespace Application.Tags.Repositories;
public interface ITagRepository
{
    Task<List<RecipeTag>> GetOrCreateTag( List<string> tagNames );
}
