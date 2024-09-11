using Application.Tags.Repositories;
using Domain.Recipes.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tags.Repository;

public class TagRepository : ITagRepository
{
    private readonly DbSet<Tag> _entities;
    public TagRepository( AppDbContext context )
    {
        _entities = context.Set<Tag>();
    }

    public async Task<List<Tag>> GetAllTagsWithRecipeTags()
    {
        return await _entities.Include( t => t.Recipes ).ToListAsync();
    }

    public async Task<List<RecipeTag>> GetRecipesTagsByTagsNames( List<string> tagNames )
    {
        return await _entities.Where( tag => tagNames.Contains( tag.Name ) )
            .SelectMany( tag => tag.Recipes )
            .Select( recipeTag => new RecipeTag( recipeTag.TagId, recipeTag.Tag ) )
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<string>> GetRandomTagsNames( int tagsCount )
    {
        return await _entities.Select( tag => tag.Name )
            .OrderBy( x => Guid.NewGuid() )
            .Take( tagsCount )
            .ToListAsync();
    }

    public async Task CreateTags( List<Tag> newTags )
    {
        await _entities.AddRangeAsync( newTags );
    }

    public void RemoveTags( List<Tag> tags )
    {
        _entities.RemoveRange( tags );
    }
}
