using Domain.Recipes.Entities;

namespace Application.Likes.Repositories;

public interface ILikeRepository
{
    Task AddLike( Like like );
    void RemoveLike( Like like );
    Task<Like> GetLikeConnectionByRecipeAndUserId( int userId, int recipeId );
}
