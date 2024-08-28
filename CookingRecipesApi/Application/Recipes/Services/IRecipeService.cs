using Application.Recipes.Entities;
using Application.ResultObject;

namespace Application.Recipes.Services;

public interface IRecipeService
{
    Task<Result> CreateRecipe( Recipe recipe );
    Task UpdateRecipe( Recipe recipe, int recipeId );
    Task RemoveRecipe( int recipeId );
    Task<IReadOnlyList<OverviewRecipe>> GetRecipes( int pageNumber, int authorId, string searchString );
    Task<IReadOnlyList<OverviewRecipe>> GetFavouriteRecipes( int pageNumber, int authorId );
    Task<MostLikedRecipe> GetMostLikedRecipe();
    Task<CompleteRecipe> GetRecipeByIdIncludingDependentEntities( int recipeId, int authorId );
    Task<bool> HasAccessToRecipe( int recipeId, int authorId );
}
