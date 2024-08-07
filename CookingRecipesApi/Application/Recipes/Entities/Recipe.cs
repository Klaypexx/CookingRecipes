using Microsoft.AspNetCore.Http;

namespace Application.Recipes.Entities;
public class Recipe
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CookingTime { get; set; }
    public int Portion { get; set; }
    public int AuthorId { get; set; }
    public IFormFile Avatar { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Step> Steps { get; set; }
}
