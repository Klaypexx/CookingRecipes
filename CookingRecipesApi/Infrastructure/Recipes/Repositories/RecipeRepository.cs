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

    public async Task<List<Recipe>> GetAllUserRecipes( int userId )
    {
        return await _entities
         .Where( recipe => recipe.AuthorId == userId )
         .Include( recipe => recipe.Ingredients )
         .Include( recipe => recipe.Steps )
         .Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .ToListAsync();
    }
}
