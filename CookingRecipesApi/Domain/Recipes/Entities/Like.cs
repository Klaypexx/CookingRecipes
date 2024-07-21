using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;
public class Like
{
    public string UserId { get; set; }
    public string RecipeId { get; set; }
    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
