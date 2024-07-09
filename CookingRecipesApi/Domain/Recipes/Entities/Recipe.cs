using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;
public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public TimeOnly? CookingTime { get; set; }
    public int? Portion { get; set; }
    public int? Like { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Step> Steps { get; set; }
    public User Author { get; set; }
    public List<User> FavouritedBy { get; set; }

}
