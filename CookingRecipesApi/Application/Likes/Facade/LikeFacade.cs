using Application.Likes.Services;
using Application.ResultObject;

namespace Application.Likes.Facade;

public class LikeFacade : ILikeFacade
{
    private readonly ILikeService _likeService;

    public LikeFacade( ILikeService likeService )
    {
        _likeService = likeService;
    }

    public async Task<Result> AddLike( int userId, int recipeId )
    {
        try
        {
            await _likeService.AddLike( userId, recipeId );

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result> RemoveLike( int userId, int recipeId )
    {
        try
        {
            await _likeService.RemoveLike( userId, recipeId );

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }
}
