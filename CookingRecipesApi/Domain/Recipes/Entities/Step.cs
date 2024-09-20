namespace Domain.Recipes.Entities;

public class Step
{
    public int Id { get; }
    public string Description { get; }
    public int RecipeId { get; }
    public Recipe Recipe { get; }

    public Step( string description )
    {
        Description = description;
    }
}
