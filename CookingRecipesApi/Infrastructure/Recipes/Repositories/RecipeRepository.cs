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

    public async Task<IReadOnlyList<Recipe>> GetRecipes( int skipRange, int pageAmount, string searchString )
    {
        return await _entities.Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .Include( recipe => recipe.Author )
         .Include( recipe => recipe.Likes )
         .Include( recipe => recipe.FavouriteRecipes )
         .Where( recipe => string.IsNullOrEmpty( searchString )
                || recipe.Name.ToLower().Contains( searchString )
                || recipe.Tags.Any( tag => tag.Tag.Name.ToLower().Contains( searchString ) ) )
         .Skip( skipRange )
         .Take( pageAmount )
         .ToListAsync();
    }

    public async Task<IReadOnlyList<Recipe>> GetFavouriteRecipes( int skipRange, int pageAmount, int authorId )
    {
        return await _entities.Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .Include( recipe => recipe.Author )
         .Include( recipe => recipe.Likes )
         .Include( recipe => recipe.FavouriteRecipes )
         .Where( recipe => recipe.FavouriteRecipes.Any( favouriteRecipe => favouriteRecipe.UserId == authorId ) )
         .Skip( skipRange )
         .Take( pageAmount )
         .ToListAsync();
    }

    public async Task<IReadOnlyList<Recipe>> GetUserRecipes( int skipRange, int pageAmount, int authorId )
    {
        return await _entities.Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .Include( recipe => recipe.Author )
         .Include( recipe => recipe.Likes )
         .Include( recipe => recipe.FavouriteRecipes )
         .Where( recipe => recipe.AuthorId == authorId )
         .Skip( skipRange )
         .Take( pageAmount )
         .ToListAsync();
    }

    public async Task<Recipe> GetMostLikedRecipe()
    {
        return await _entities.Include( recipe => recipe.Author )
            .Include( recipe => recipe.Likes )
            .OrderByDescending( recipe => recipe.Likes.Count )
            .FirstOrDefaultAsync();
    }

    public async Task<Recipe> GetRecipeByIdIncludingDependentEntities( int recipeId )
    {
        return await _entities.Where( recipe => recipe.Id == recipeId )
         .Include( recipe => recipe.Tags )
         .ThenInclude( recipeTag => recipeTag.Tag )
         .Include( recipe => recipe.Ingredients )
         .Include( recipe => recipe.Steps )
         .Include( recipe => recipe.Author )
         .Include( recipe => recipe.Likes )
         .Include( recipe => recipe.FavouriteRecipes )
         .FirstOrDefaultAsync();
    }

    public async Task<Recipe> GetRecipeById( int recipeId )
    {
        return await _entities.Where( recipe => recipe.Id == recipeId )
            .FirstOrDefaultAsync();
    }
}
