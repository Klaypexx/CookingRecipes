namespace Domain.Recipes.Entities;

public class Ingredient
{
    public int Id { get; }
    public string Name { get; }
    public string Product { get; }
    public int RecipeId { get; }
    public Recipe Recipe { get; }

    public Ingredient( string name, string product )
    {
        Name = name;
        Product = product;
    }
}
