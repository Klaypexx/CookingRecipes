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

    public async Task CreateTags( List<Tag> newTags )
    {
        await _tagRepository.CreateTags( newTags );
    }

    public async Task<List<Tag>> GetExistingTagsByName( List<string> tagNames )
    {
        return await _tagRepository.GetExistingTagsByName( tagNames );
    }

    public async Task<List<Tag>> GetTagsByNameWithRecipes( List<string> tagsName )
    {
        return await _tagRepository.GetTagsByNameWithRecipes( tagsName );
    }

    public async Task RemoveTags( int recipeId, List<string> tagsName )
    {
        List<Tag> tags = await GetTagsByNameWithRecipes( tagsName );
        List<Tag> tagsToDelete = tags.Where( tag => tag?.Recipes.Count( r => r.RecipeId != recipeId ) == 0 ).ToList();
        if ( tagsToDelete?.Count > 0 )
        {
            _tagRepository.RemoveTags( tagsToDelete );
        }
    }
}
