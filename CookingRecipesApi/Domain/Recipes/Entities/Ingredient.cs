namespace Domain.Recipes.Entities;

public class Ingredient
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Product { get; set; }
    public Recipe Recipe { get; set; }
}
