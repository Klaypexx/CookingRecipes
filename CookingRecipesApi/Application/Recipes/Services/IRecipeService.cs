using Application.Recipes.Entities;

namespace Application.Recipes.Services;

public interface IRecipeService
{
    Task CreateRecipe( Recipe recipe );
    Task UpdateRecipe( Recipe recipe, int recipeId );
    Task RemoveRecipe( int recipeId, int authorId );
    Task<RecipesData<OverviewRecipe>> GetRecipes( int authorId, int pageNumber, string searchString );
    Task<RecipesData<OverviewRecipe>> GetFavouriteRecipeByAuthorId( int authorId, int pageNumber );
    Task<RecipesData<OverviewRecipe>> GetRecipeByAuthorId( int authorId, int pageNumber );
    Task<MostLikedRecipe> GetMostLikedRecipe();
    Task<CompleteRecipe> GetRecipeByIdIncludingDependentEntities( int recipeId, int authorId );
}
