using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

public class FavouriteRecipe
{
    public int UserId { get; }
    public int RecipeId { get; }
    public User User { get; }
    public Recipe Recipe { get; }

    public FavouriteRecipe( int userId, int recipeId )
    {
        UserId = userId;
        RecipeId = recipeId;
    }
}
