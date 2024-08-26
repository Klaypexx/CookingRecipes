using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

public class FavouriteRecipe
{
    public int UserId { get; private set; }
    public int RecipeId { get; private set; }
    public User User { get; private set; }
    public Recipe Recipe { get; private set; }

    public FavouriteRecipe( int userId, int recipeId )
    {
        UserId = userId;
        RecipeId = recipeId;
    }
}
