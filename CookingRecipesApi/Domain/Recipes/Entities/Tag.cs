namespace Domain.Recipes.Entities;

public class Tag
{
    public int Id { get; }
    public string Name { get; }
    public List<RecipeTag> Recipes { get; }

    public Tag( string name )
    {
        Name = name;
    }
}
