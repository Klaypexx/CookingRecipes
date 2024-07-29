using Application.Tags.Repositories;
using Domain.Recipes.Entities;

namespace Application.Tags.Services;
public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    public TagService( ITagRepository tagRepository )
    {
        _tagRepository = tagRepository;
    }
    public async Task<List<RecipeTag>> GetOrCreateTag( List<string> tagNames )
    {
        return await _tagRepository.GetOrCreateTag( tagNames );
    }
}
