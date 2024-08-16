namespace Domain.Recipes.Entities;
public class Step
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public int RecipeId { get; private set; }
    public Recipe Recipe { get; private set; }

    public Step( string description )
    {
        Description = description;
    }
}
