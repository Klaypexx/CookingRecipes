using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Recipes.Repositories;
using Domain.Auth.Entities;
using Domain.Recipes.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Recipes.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly DbSet<Recipe> _entitiesRecipe;
    private readonly DbSet<Tag> _entitiesTag;

    public RecipeRepository( AppDbContext context )
    {
        _entitiesRecipe = context.Set<Recipe>();
        _entitiesTag = context.Set<Tag>();
    }
    public async Task CreateRecipe( Recipe recipe )
    {
        await _entitiesRecipe.AddAsync( recipe );
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

    public async Task<List<Recipe>> GetAllUserRecipes( int userId )
    {
        return await _entitiesRecipe
         .Where( recipe => recipe.AuthorId == userId )
         .Include( recipe => recipe.Ingredients )
         .Include( recipe => recipe.Steps )
         .Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .ToListAsync();
    }
}
