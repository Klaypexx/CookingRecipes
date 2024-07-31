using Application.Recipes.Repositories;
using Domain.Recipes.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Recipes.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly DbSet<Recipe> _entities;

    public RecipeRepository( AppDbContext context )
    {
        _entities = context.Set<Recipe>();
    }
    public async Task CreateRecipe( Recipe recipe )
    {
        await _entities.AddAsync( recipe );
    }

    public async Task<List<Recipe>> GetAllRecipes( int page )
    {
        return await _entities
         .Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .Include( recipe => recipe.Author )
         .Skip( ( page - 1 ) * 4 )
         .Take( 4 )
         .ToListAsync();
    }
}
