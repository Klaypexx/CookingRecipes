using Application.Files.Services;
using Application.Foundation;
using Application.Recipes.Entities;
using Application.Recipes.Repositories;
using Application.Tags.Services;
using RecipeDomain = Domain.Recipes.Entities.Recipe;
using Application.Recipes.Extensions;
using Application.ResultObject;
using Application.Validation;

namespace Application.Recipes.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITagService _tagService;
    private readonly IFileService _fileService;
    private readonly IRecipeCreator _recipeCreator;
    private readonly IValidator<Recipe> _recipeValidator;
    private readonly IUnitOfWork _unitOfWork;

    private readonly int _pageAmount = 5;

    public RecipeService( IRecipeRepository recipeRepository,
        ITagService tagService,
        IFileService fileService,
        IRecipeCreator recipeCreator,
        IValidator<Recipe> recipeValidator,
        IUnitOfWork unitOfWork )
    {
        _recipeRepository = recipeRepository;
        _tagService = tagService;
        _fileService = fileService;
        _recipeCreator = recipeCreator;
        _recipeValidator = recipeValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> CreateRecipe( Recipe recipe )
    {
        try
        {
            Result result = _recipeValidator.Validate( recipe );

            if ( !result.IsSuccess )
            {
                return result;
            }

            string pathToFile = await _fileService.SaveImage( recipe.Avatar );
            RecipeDomain recipeDomain = _recipeCreator.Create( recipe, pathToFile );

            await _tagService.ActualizeTags( recipeDomain );
            await _recipeRepository.CreateRecipe( recipeDomain );
            await _unitOfWork.Save();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result> UpdateRecipe( Recipe actualRecipe, int recipeId )
    {
        try
        {
            bool hasAccess = await HasAccessToRecipe( recipeId, actualRecipe.AuthorId );

            if ( !hasAccess )
            {
                throw new UnauthorizedAccessException( "Нет доступа" );
            }

            RecipeDomain oldRecipe = await _recipeRepository.GetRecipeByIdIncludingDependentEntities( recipeId );

            string pathToFile = await _fileService.UpdateImage( actualRecipe.Avatar, oldRecipe.Avatar );

            RecipeDomain recipe = _recipeCreator.Create( actualRecipe, pathToFile );

            await _tagService.ActualizeTags( recipe );
            oldRecipe.UpdateRecipe( recipe );
            await _unitOfWork.Save();

            await RemoveUnusedTags();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result> RemoveRecipe( int recipeId, int authorId )
    {
        try
        {
            bool hasAccess = await HasAccessToRecipe( recipeId, authorId );

            if ( !hasAccess )
            {
                throw new UnauthorizedAccessException( "Нет доступа" );
            }

            RecipeDomain recipe = await _recipeRepository.GetRecipeByIdIncludingDependentEntities( recipeId );

            _fileService.RemoveImage( recipe.Avatar );

            recipe.Likes.Clear();

            recipe.FavouriteRecipes.Clear();

            recipe.Tags.Clear();

            _recipeRepository.RemoveRecipe( recipe );
            await _unitOfWork.Save();

            await RemoveUnusedTags();

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result<RecipesData<OverviewRecipe>>> GetRecipes( int authorId, int pageNumber, string searchString )
    {
        try
        {
            int skipRange = ( pageNumber - 1 ) * ( _pageAmount - 1 );

            IReadOnlyList<RecipeDomain> recipes = await _recipeRepository.GetRecipes( skipRange, _pageAmount, searchString.ToLower() );

            bool isLastRecipes = recipes.Count <= 4;

            IReadOnlyList<RecipeDomain> currentRecipesCountToTake = recipes.Take( _pageAmount - 1 ).ToList();

            RecipesData<OverviewRecipe> recipeData = new( currentRecipesCountToTake.ToOverviewRecipe( authorId ), isLastRecipes );

            return new Result<RecipesData<OverviewRecipe>>( recipeData );
        }
        catch ( Exception e )
        {
            return new Result<RecipesData<OverviewRecipe>>( new Error( e.Message ) );
        }
    }

    public async Task<Result<RecipesData<OverviewRecipe>>> GetFavouriteRecipeByAuthorId( int authorId, int pageNumber )
    {
        try
        {
            int skipRange = ( pageNumber - 1 ) * ( _pageAmount - 1 );

            IReadOnlyList<RecipeDomain> recipes = await _recipeRepository.GetFavouriteRecipeByAuthorId( authorId, skipRange, _pageAmount );

            bool isLastRecipes = recipes.Count <= 4;

            IReadOnlyList<RecipeDomain> currentRecipesCountToTake = recipes.Take( _pageAmount - 1 ).ToList();

            RecipesData<OverviewRecipe> recipeData = new( currentRecipesCountToTake.ToOverviewRecipe( authorId ), isLastRecipes );

            return new Result<RecipesData<OverviewRecipe>>( recipeData );
        }
        catch ( Exception e )
        {
            return new Result<RecipesData<OverviewRecipe>>( new Error( e.Message ) );
        }
    }

    public async Task<Result<RecipesData<OverviewRecipe>>> GetRecipeByAuthorId( int authorId, int pageNumber )
    {
        try
        {
            int skipRange = ( pageNumber - 1 ) * ( _pageAmount - 1 );

            IReadOnlyList<RecipeDomain> recipes = await _recipeRepository.GetRecipeByAuthorId( authorId, skipRange, _pageAmount );

            bool isLastRecipes = recipes.Count <= 4;

            IReadOnlyList<RecipeDomain> currentRecipesCountToTake = recipes.Take( _pageAmount - 1 ).ToList();

            RecipesData<OverviewRecipe> recipeData = new( currentRecipesCountToTake.ToOverviewRecipe( authorId ), isLastRecipes );

            return new Result<RecipesData<OverviewRecipe>>( recipeData );
        }
        catch ( Exception e )
        {
            return new Result<RecipesData<OverviewRecipe>>( new Error( e.Message ) );
        }
    }

    public async Task<Result<MostLikedRecipe>> GetMostLikedRecipe()
    {
        try
        {
            RecipeDomain recipe = await _recipeRepository.GetMostLikedRecipe();

            if ( recipe == null )
            {
                return null;
            }

            return new Result<MostLikedRecipe>( recipe.ToMostLikedRecipe() );
        }
        catch ( Exception e )
        {
            return new Result<MostLikedRecipe>( new Error( e.Message ) );
        }
    }

    public async Task<Result<CompleteRecipe>> GetRecipeByIdIncludingDependentEntities( int recipeId, int authorId )
    {
        try
        {
            RecipeDomain recipe = await _recipeRepository.GetRecipeByIdIncludingDependentEntities( recipeId );

            return new Result<CompleteRecipe>( recipe.ToCompleteRecipe( authorId ) );
        }
        catch ( Exception e )
        {
            return new Result<CompleteRecipe>( new Error( e.Message ) );
        }
    }

    private async Task<bool> HasAccessToRecipe( int recipeId, int authorId )
    {
        RecipeDomain recipe = await _recipeRepository.GetRecipeById( recipeId );

        return recipe.AuthorId == authorId;
    }

    private async Task RemoveUnusedTags()
    {
        await _tagService.RemoveUnusedTags();
        await _unitOfWork.Save();
    }
}
