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

    public void UpdateRecipe( Recipe recipe )
    {
        _entities.Update( recipe );
    }

    public void RemoveRecipe( Recipe recipe )
    {
        _entities.Remove( recipe );
    }

    public async Task<IReadOnlyList<Recipe>> GetRecipes( int skipRange, int pageAmount )
    {
        return await _entities.Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .Include( recipe => recipe.Author )
         .OrderBy( recipe => recipe.Id )
         .Skip( skipRange )
         .Take( pageAmount )
         .ToListAsync();
    }

    public async Task<Recipe> GetRecipeById( int recipeId )
    {
        return await _entities.Where( recipe => recipe.Id == recipeId )
         .Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .Include( recipe => recipe.Ingredients )
         .Include( recipe => recipe.Steps )
         .Include( recipe => recipe.Author )
         .FirstOrDefaultAsync();
    }

    public async Task<Recipe> GetByIdWithTag( int recipeId )
    {
        return await _entities.Where( recipe => recipe.Id == recipeId )
            .Include( recipe => recipe.Tags )
            .ThenInclude( tag => tag.Tag )
            .FirstOrDefaultAsync();
    }

    public async Task<Recipe> GetById( int recipeId )
    {
        return await _entities.Where( recipe => recipe.Id == recipeId )
            .FirstOrDefaultAsync();
    }
}
