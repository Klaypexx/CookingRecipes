namespace Domain.Recipes.Entities;

public class Tag
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<RecipeTag> Recipes { get; private set; }

    public Tag( string name )
    {
        Name = name;
    }
}
