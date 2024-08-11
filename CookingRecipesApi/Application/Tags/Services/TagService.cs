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

    public async Task<List<Tag>> GetTagsByNames( List<string> tagNames )
    {
        return await _tagRepository.GetTagsByNames( tagNames );
    }

    public async Task<List<Tag>> GetTagsByIdWithRecipes( List<int> tagsId )
    {
        return await _tagRepository.GetTagsByIdWithRecipes( tagsId );
    }

    public async Task RemoveTags( int recipeId, List<int> tagsId )
    {
        List<Tag> tags = await GetTagsByIdWithRecipes( tagsId );
        List<Tag> tagsToDelete = tags.Where( tag => tag?.Recipes.Count( r => r.RecipeId != recipeId ) == 0 ).ToList();
        if ( tagsToDelete?.Count > 0 )
        {
            _tagRepository.RemoveTags( tagsToDelete );
        }
    }
}
