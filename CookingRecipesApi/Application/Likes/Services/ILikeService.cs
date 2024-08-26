using Domain.Recipes.Entities;

namespace Application.Likes.Services;
public interface ILikeService
{
    Task AddLike( int userId, int recipeId );
    Task RemoveLike( int userId, int recipeId );
    IReadOnlyList<int> GetRecipesIdsThatUserLike( int userId, IReadOnlyList<Recipe> recipes );
    bool HaveRecipeLikeFromUser( int userId, Recipe recipe );
}
