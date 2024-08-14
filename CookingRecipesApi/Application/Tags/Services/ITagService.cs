using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public interface ITagService
{
    Task ActualizeTags( Recipe recipe );
    Task CreatingLinksWithTags( Recipe recipe );
    Task RemoveTagsLinks( Recipe recipe );
    Task RemoveUnusedTags();
}
