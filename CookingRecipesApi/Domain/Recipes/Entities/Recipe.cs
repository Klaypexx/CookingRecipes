using System.ComponentModel.DataAnnotations.Schema;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;
public class Recipe
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TimeOnly? CookingTime { get; set; }
    public int? Portion { get; set; }
    public string Avatar { get; set; }
    public string AuthorId { get; set; }
    public List<Like> LikesCount { get; set; }
    public List<RecipeTag> Tags { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Step> Steps { get; set; }
    public User Author { get; set; }
    public List<FavouriteRecipe> FavouritedBy { get; set; }

}
