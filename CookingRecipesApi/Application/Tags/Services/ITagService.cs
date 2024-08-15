using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task ActualizeTags( Recipe recipe );
    void RemoveTagsLinks( Recipe recipe );
    Task RemoveUnusedTags();
}
