using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;

public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    void UpdateRecipe( Recipe recipe );
    void RemoveRecipe( Recipe recipe );
    Task<IReadOnlyList<Recipe>> GetRecipes( int skipRange, int pageAmount, string searchString );
    Task<IReadOnlyList<Recipe>> GetFavouriteRecipeByAuthorId( int authorId, int skipRange, int pageAmount );
    Task<IReadOnlyList<Recipe>> GetRecipeByAuthorId( int authorId, int skipRange, int pageAmount );
    Task<Recipe> GetMostLikedRecipe();
    Task<Recipe> GetRecipeByIdIncludingDependentEntities( int recipeId );
    Task<Recipe> GetRecipeById( int recipeId );
}
