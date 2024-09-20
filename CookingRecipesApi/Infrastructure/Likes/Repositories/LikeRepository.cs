using Application.Likes.Repositories;
using Domain.Recipes.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Likes.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly DbSet<Like> _entities;

    public LikeRepository( AppDbContext context )
    {
        _entities = context.Set<Like>();
    }
    public async Task AddLike( Like like )
    {
        await _entities.AddAsync( like );
    }

    public void RemoveLike( Like like )
    {
        _entities.Remove( like );
    }

    public async Task<Like> GetLikeConnectionByRecipeAndUserId( int userId, int recipeId )
    {
        return await _entities.Where( like => like.UserId == userId && like.RecipeId == recipeId )
            .FirstOrDefaultAsync();
    }
}

