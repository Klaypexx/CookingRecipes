using Application.ResultObject;

namespace Application.Likes.Facade;

public interface ILikeFacade
{
    Task<Result> AddLike( int userId, int recipeId );
    Task<Result> RemoveLike( int userId, int recipeId );
}
