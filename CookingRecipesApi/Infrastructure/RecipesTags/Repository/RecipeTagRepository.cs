using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RecipesTags.Repositories;
using Domain.Recipes.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RecipesTags.Repository;
public class RecipeTagRepository : IRecipeTagRepository
{
    private readonly DbSet<RecipeTag> _entities;
    public RecipeTagRepository( AppDbContext context )
    {
        _entities = context.Set<RecipeTag>();
    }
    public async Task<List<RecipeTag>> GetRecipeTagsByRecipeIdAndTagIds( int recipeId, List<int> tagsId )
    {
        return await _entities
            .Where( rt => rt.RecipeId == recipeId && tagsId.Contains( rt.TagId ) )
            .ToListAsync();
    }

    public void RemoveConnections( List<RecipeTag> recipeTag )
    {
        _entities.RemoveRange( recipeTag );
    }
}
