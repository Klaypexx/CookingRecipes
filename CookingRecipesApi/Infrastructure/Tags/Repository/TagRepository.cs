using Application.Tags.Repositories;
using Domain.Recipes.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tags.Repository;
public class TagRepository : ITagRepository
{
    private readonly DbSet<Tag> _entitiesTag;
    public TagRepository( AppDbContext context )
    {
        _entitiesTag = context.Set<Tag>();
    }
    public async Task<List<Tag>> GetExistingTags( List<string> tagNames )
    {
        return await _entitiesTag
            .Where( t => tagNames.Contains( t.Name ) )
            .ToListAsync();
    }

    public async Task CreateTags( List<Tag> newTags )
    {
        await _entitiesTag.AddRangeAsync( newTags );
    }
}
