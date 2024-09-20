namespace Application.Likes.Services;

public interface ILikeService
{
    Task AddLike( int userId, int recipeId );
    Task RemoveLike( int userId, int recipeId );
}
