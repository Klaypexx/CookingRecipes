using Application.Foundation;
using Application.Likes.Services;
using Application.ResultObject;

namespace Application.Likes.Facade;

public class LikeFacade : ILikeFacade
{
    private readonly ILikeService _likeService;
    private readonly IUnitOfWork _unitOfWork;

    public LikeFacade( ILikeService likeService, IUnitOfWork unitOfWork )
    {
        _likeService = likeService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> AddLike( int userId, int recipeId )
    {
        try
        {
            await _likeService.AddLike( userId, recipeId );

            await _unitOfWork.Save();

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

            await _unitOfWork.Save();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }
}
