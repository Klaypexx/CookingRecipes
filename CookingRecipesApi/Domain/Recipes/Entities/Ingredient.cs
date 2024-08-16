namespace Domain.Recipes.Entities;

public class Ingredient
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Product { get; private set; }
    public int RecipeId { get; private set; }
    public Recipe Recipe { get; private set; }

    public Ingredient( string name, string product )
    {
        Name = name;
        Product = product;
    }
}
