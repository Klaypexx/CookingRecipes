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

    public async Task CreateTags( List<Tag> newTags )
    {
        await _entities.AddRangeAsync( newTags );
    }
}
