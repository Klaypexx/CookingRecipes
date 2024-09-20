using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

public class Like
{
    public int UserId { get; }
    public int RecipeId { get; }
    public User User { get; }
    public Recipe Recipe { get; }

    public Like( int userId, int recipeId )
    {
        UserId = userId;
        RecipeId = recipeId;
    }
}
