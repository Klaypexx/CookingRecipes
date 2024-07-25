using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Recipes.Entities;

public class Ingredient
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Product { get; set; }
    public string RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
