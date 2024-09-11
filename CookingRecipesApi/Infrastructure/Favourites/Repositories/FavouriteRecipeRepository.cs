using Application.Favourites.Repositories;
using Domain.Recipes.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Favourites.Repositories;

public class FavouriteRecipeRepository : IFavouriteRecipeRepository
{
    private readonly DbSet<FavouriteRecipe> _entities;

    public FavouriteRecipeRepository( AppDbContext context )
    {
        _entities = context.Set<FavouriteRecipe>();
    }

    public async Task AddFavouriteRecipe( FavouriteRecipe favouriteRecipe )
    {
        await _entities.AddAsync( favouriteRecipe );
    }

    public void RemoveFavouriteRecipe( FavouriteRecipe favouriteRecipe )
    {
        _entities.Remove( favouriteRecipe );
    }

    public async Task<FavouriteRecipe> GetFavouriteRecipe( int userId, int recipeId )
    {
        return await _entities.Where( favouriteRecipe => favouriteRecipe.UserId == userId && favouriteRecipe.RecipeId == recipeId )
            .FirstOrDefaultAsync();
    }
}
