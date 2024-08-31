using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;

public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    void UpdateRecipe( Recipe recipe );
    void RemoveRecipe( Recipe recipe );
    Task<IReadOnlyList<Recipe>> GetRecipes( int skipRange, int pageAmount, string searchString );
    Task<IReadOnlyList<Recipe>> GetFavouriteRecipes( int skipRange, int pageAmount, int authorId );
    Task<IReadOnlyList<Recipe>> GetUserRecipes( int skipRange, int pageAmount, int authorId );
    Task<Recipe> GetMostLikedRecipe();
    Task<Recipe> GetRecipeByIdIncludingDependentEntities( int recipeId );
    Task<Recipe> GetRecipeById( int recipeId );
}
