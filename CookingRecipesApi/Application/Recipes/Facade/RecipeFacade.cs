using Application.Recipes.Entities;
using Application.Recipes.Repositories;
using Application.Recipes.Services;
using Application.ResultObject;
using Application.Validation;
using RecipeDomain = Domain.Recipes.Entities.Recipe;

namespace Application.Recipes.Facade;

public class RecipeFacade : IRecipeFacade
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipeService _recipeService;
    private readonly IValidator<Recipe> _recipeValidator;

    public RecipeFacade( IRecipeRepository recipeRepository,
        IRecipeService recipeService,
        IValidator<Recipe> recipeValidator )
    {
        _recipeRepository = recipeRepository;
        _recipeService = recipeService;
        _recipeValidator = recipeValidator;
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

            await _recipeService.CreateRecipe( recipe );

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

            await _recipeService.UpdateRecipe( actualRecipe, recipeId );

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

            await _recipeService.RemoveRecipe( recipeId, authorId );

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
            RecipesData<OverviewRecipe> recipeData = await _recipeService.GetRecipes( authorId, pageNumber, searchString );

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
            RecipesData<OverviewRecipe> recipeData = await _recipeService.GetFavouriteRecipeByAuthorId( authorId, pageNumber );

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
            RecipesData<OverviewRecipe> recipeData = await _recipeService.GetRecipeByAuthorId( authorId, pageNumber );

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
            MostLikedRecipe recipe = await _recipeService.GetMostLikedRecipe();

            return new Result<MostLikedRecipe>( recipe );
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
            CompleteRecipe recipe = await _recipeService.GetRecipeByIdIncludingDependentEntities( recipeId, authorId );

            return new Result<CompleteRecipe>( recipe );
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
}
