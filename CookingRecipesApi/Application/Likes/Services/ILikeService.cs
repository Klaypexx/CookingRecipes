using Application.ResultObject;
using Domain.Recipes.Entities;

namespace Application.Likes.Services;

public interface ILikeService
{
    Task<Result> AddLike( int userId, int recipeId );
    Task<Result> RemoveLike( int userId, int recipeId );
}
