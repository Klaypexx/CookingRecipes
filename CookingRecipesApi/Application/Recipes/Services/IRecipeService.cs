using Application.Recipes.Entities;
using Application.ResultObject;

namespace Application.Recipes.Services;

public interface IRecipeService
{
    Task<Result> CreateRecipe( Recipe recipe );
    Task<Result> UpdateRecipe( Recipe recipe, int recipeId );
    Task<Result> RemoveRecipe( int recipeId, int authorId );
    Task<Result<RecipesData<OverviewRecipe>>> GetRecipes( int pageNumber, int authorId, string searchString );
    Task<Result<RecipesData<OverviewRecipe>>> GetFavouriteRecipes( int pageNumber, int authorId );
    Task<Result<RecipesData<OverviewRecipe>>> GetUserRecipes( int pageNumber, int authorId );
    Task<Result<MostLikedRecipe>> GetMostLikedRecipe();
    Task<Result<CompleteRecipe>> GetRecipeByIdIncludingDependentEntities( int recipeId, int authorId );
}
