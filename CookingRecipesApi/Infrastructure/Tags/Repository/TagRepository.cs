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
    public async Task<List<Tag>> GetExistingTags( List<string> tagNames )
    {
        return await _entities
            .Where( t => tagNames.Contains( t.Name ) )
            .ToListAsync();
    }

    public async Task<List<Tag>> GetTagsByIdWithRecipes( List<int> tagsId )
    {
        return await _entities
        .Where( t => tagsId.Contains( t.Id ) )
        .Include( t => t.Recipes )
        .ToListAsync();
    }

    public async Task<List<Tag>> GetTagsToDelete( List<Tag> tags, int recipeId )
    {
        return tags.Where( t => t.Recipes.Count( r => r.RecipeId != recipeId ) == 0 ).ToList(); ;
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
