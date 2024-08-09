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
    public async Task<List<RecipeTag>> GetTags( List<string> tagNames )
    {
        if ( tagNames == null )
        {
            return null;
        }

        List<Tag> tags = await _tagRepository.GetExistingTags( tagNames );

        List<Tag> newTags = tagNames
            .Where( name => !tags.Any( t => t.Name.ToLower() == name.ToLower() ) )
            .Select( name => new Tag { Name = name.ToLower() } )
            .ToList();

        await _tagRepository.CreateTags( newTags );

        return tags
            .Select( t => new RecipeTag { Tag = t } )
            .Concat( newTags.Select( t => new RecipeTag { Tag = t } ) )
            .ToList();
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
