using Application.Foundation;
using Application.Likes.Repositories;
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
    public async Task AddLike( int userId, int recipeId )
    {
        Like likeToAdd = new( userId, recipeId );

        await _likeRepository.AddLike( likeToAdd );

        await _unitOfWork.Save();
    }

    public async Task RemoveLike( int userId, int recipeId )
    {
        Like likeToRemove = await _likeRepository.GetLikeConnectionByRecipeAndUserId( userId, recipeId );

        _likeRepository.RemoveLike( likeToRemove );

        await _unitOfWork.Save();
    }

    public List<int> GetRecipesIdsThatUserLike( int userId, List<Recipe> recipes )
    {
        return recipes
                .Where( recipe => recipe.Likes.Any( like => like.UserId == userId ) )
                .Select( recipe => recipe.Id )
                .ToList();
    }

    public bool HaveRecipeLikeFromUser( int userId, Recipe recipe )
    {
        return recipe.Likes.Any( like => like.UserId == userId );
    }
}
