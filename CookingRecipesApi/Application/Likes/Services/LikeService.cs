using Application.Likes.Repositories;
using Domain.Recipes.Entities;

namespace Application.Likes.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;

    public LikeService( ILikeRepository likeRepository )
    {
        _likeRepository = likeRepository;
    }

    public async Task AddLike( int userId, int recipeId )
    {
        Like likeToAdd = new( userId, recipeId );

        await _likeRepository.AddLike( likeToAdd );
    }

    public async Task RemoveLike( int userId, int recipeId )
    {
        Like likeToRemove = await _likeRepository.GetLikeConnectionByRecipeAndUserId( userId, recipeId );

        _likeRepository.RemoveLike( likeToRemove );
    }
}
