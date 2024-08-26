using Application.Files.Services;
using Application.Foundation;
using Application.Recipes.Entities;
using Application.Recipes.Repositories;
using Application.Tags.Services;
using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Recipes.Extensions;
using Application.Likes.Services;
using Application.Favourites.Services;

namespace Application.Recipes.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    private readonly IFileService _fileService;
    private readonly ILikeService _likeService;
    private readonly IFavouriteRecipeService _favouriteRecipeService;
    private readonly IRecipeCreator _recipeCreator;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeService( IRecipeRepository recipeRepository,
        ITagService tagService,
        IFileService fileService,
        ILikeService likeService,
        IFavouriteRecipeService favouriteRecipeService,
        IRecipeCreator recipeCreator,
        IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
        _fileService = fileService;
        _likeService = likeService;
        _favouriteRecipeService = favouriteRecipeService;
        _recipeCreator = recipeCreator;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRecipe( Recipe recipe )
    {
        string pathToFile = await _fileService.SaveImage( recipe.Avatar );
        RecipeDomain recipeDomain = _recipeCreator.Create( recipe, pathToFile );

        await _tagService.ActualizeTags( recipeDomain );
        await _recipeRepository.CreateRecipe( recipeDomain );
        await _unitOfWork.Save();
    }

    public async Task UpdateRecipe( Recipe actualRecipe, int recipeId )
    {
        RecipeDomain oldRecipe = await _recipeRepository.GetRecipeById( recipeId );
        string pathToFile = await _fileService.UpdateImage( actualRecipe.Avatar, oldRecipe.Avatar );
        RecipeDomain recipe = _recipeCreator.Create( actualRecipe, pathToFile );

        await _tagService.ActualizeTags( recipe );
        oldRecipe.UpdateRecipe( recipe );
        await _unitOfWork.Save();

        await RemoveUnusedTags();
    }

    public async Task RemoveRecipe( int recipeId )
    {
        RecipeDomain recipe = await _recipeRepository.GetRecipeById( recipeId );

        _fileService.RemoveImage( recipe.Avatar );

        recipe.Likes.Clear();

        recipe.FavouriteRecipes.Clear();

        recipe.Tags.Clear();

        _recipeRepository.RemoveRecipe( recipe );
        await _unitOfWork.Save();

        await RemoveUnusedTags();
    }

    private async Task RemoveUnusedTags()
    {
        await _tagService.RemoveUnusedTags();
        await _unitOfWork.Save();
    }

    public async Task<IReadOnlyList<OverviewRecipe>> GetRecipes( int pageNumber, int authorId, string searchString )
    {
        int pageAmount = 4;
        int skipRange = ( pageNumber - 1 ) * pageAmount;

        IReadOnlyList<RecipeDomain> recipes = await _recipeRepository.GetRecipes( skipRange, pageAmount, searchString.ToLower() );

        IReadOnlyList<int> likedIds = _likeService.GetRecipesIdsThatUserLike( authorId, recipes );
        IReadOnlyList<int> favouritedIds = _favouriteRecipeService.GetRecipesIdsThatUserAddToFavourite( authorId, recipes );

        return recipes.ToOverviewRecipe( likedIds, favouritedIds );
    }

    public async Task<IReadOnlyList<OverviewRecipe>> GetFavouriteRecipes( int pageNumber, int authorId )
    {
        int pageAmount = 4;
        int skipRange = ( pageNumber - 1 ) * pageAmount;

        IReadOnlyList<RecipeDomain> recipes = await _recipeRepository.GetFavouriteRecipes( skipRange, pageAmount, authorId );

        IReadOnlyList<int> likedIds = _likeService.GetRecipesIdsThatUserLike( authorId, recipes );
        IReadOnlyList<int> favouritedIds = _favouriteRecipeService.GetRecipesIdsThatUserAddToFavourite( authorId, recipes );

        return recipes.ToOverviewRecipe( likedIds, favouritedIds );
    }

    public async Task<CompleteRecipe> GetRecipeById( int recipeId, int authorId )
    {
        RecipeDomain recipe = await _recipeRepository.GetRecipeById( recipeId );

        bool isRecipeLiked = _likeService.HaveRecipeLikeConnectionFromUser( authorId, recipe );
        bool isRecipeInFavourite = _favouriteRecipeService.HaveFavouriteRecipeFromUser( authorId, recipe );

        return recipe.ToCompleteRecipe( isRecipeLiked, isRecipeInFavourite );
    }

    public async Task<bool> HasAccessToRecipe( int recipeId, int authorId )
    {
        RecipeDomain recipe = await _recipeRepository.GetById( recipeId );

        return recipe.AuthorId == authorId;
    }
}
