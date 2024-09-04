using Application.Foundation;
using Application.Likes.Repositories;
using Application.ResultObject;
using Domain.Recipes.Entities;

namespace Application.Likes.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LikeService( ILikeRepository likeRepository, IUnitOfWork unitOfWork )
    {
        _likeRepository = likeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> AddLike( int userId, int recipeId )
    {
        try
        {
            Like likeToAdd = new( userId, recipeId );

            await _likeRepository.AddLike( likeToAdd );

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
            Like likeToRemove = await _likeRepository.GetLikeConnectionByRecipeAndUserId( userId, recipeId );

            _likeRepository.RemoveLike( likeToRemove );

            await _unitOfWork.Save();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }
}
