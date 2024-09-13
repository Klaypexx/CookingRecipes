using Application.Recipes.Entities;
using Application.ResultObject;

namespace Application.Recipes.Facade;

public interface IRecipeFacade
{
    Task<Result> CreateRecipe( Recipe recipe );
    Task<Result> UpdateRecipe( Recipe recipe, int recipeId );
    Task<Result> RemoveRecipe( int recipeId, int authorId );
    Task<Result<RecipesData<OverviewRecipe>>> GetRecipes( int authorId, int pageNumber, string searchString );
    Task<Result<RecipesData<OverviewRecipe>>> GetFavouriteRecipeByAuthorId( int authorId, int pageNumber );
    Task<Result<RecipesData<OverviewRecipe>>> GetRecipeByAuthorId( int authorId, int pageNumber );
    Task<Result<MostLikedRecipe>> GetMostLikedRecipe();
    Task<Result<CompleteRecipe>> GetRecipeByIdIncludingDependentEntities( int recipeId, int authorId );
}
