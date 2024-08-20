using Domain.Recipes.Entities;

namespace Application.Likes.Services;
public interface ILikeService
{
    Task AddLike( int userId, int recipeId );
    Task RemoveLike( int userId, int recipeId );
    List<int> GetRecipesIdsThatUserLike( int userId, List<Recipe> recipes );
    bool HaveRecipeLikeFromUser( int userId, Recipe recipe );
}
