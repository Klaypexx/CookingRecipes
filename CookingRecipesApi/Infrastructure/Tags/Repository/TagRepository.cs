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
    public async Task<List<RecipeTag>> GetOrCreateTag( List<string> tagNames )
    {
        var tags = await _entitiesTag
            .Where( t => tagNames.Contains( t.Name ) )
            .ToListAsync();

        var newTags = tagNames
            .Where( name => !tags.Any( t => t.Name == name ) )
            .Select( name => new Tag { Name = name } )
            .ToList();

        _entitiesTag.AddRange( newTags );

        return tags
            .Select( t => new RecipeTag { Tag = t } )
            .Concat( newTags.Select( t => new RecipeTag { Tag = t } ) )
            .ToList();
    }
}
