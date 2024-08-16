using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;
public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CookingTime { get; set; }
    public int Portion { get; set; }
    public string Avatar { get; set; }
    public int AuthorId { get; set; }
    public List<Like> Likes { get; set; }
    public List<RecipeTag> Tags { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Step> Steps { get; set; }
    public User Author { get; set; }
    public List<FavouriteRecipe> FavouriteRecipes { get; set; }

    public void UpdateRecipe( Recipe newRecipe )
    {
        Name = newRecipe.Name;
        Description = newRecipe.Description;
        CookingTime = newRecipe.CookingTime;
        Portion = newRecipe.Portion;
        Avatar = newRecipe.Avatar;
        Ingredients = newRecipe.Ingredients.Select( ingredientDto => new Ingredient
        {
            Name = ingredientDto.Name,
            Product = ingredientDto.Product
        } ).ToList();
        Steps = newRecipe.Steps.Select( stepDto => new Step
        {
            Description = stepDto.Description,
        } ).ToList();
        Tags = newRecipe.Tags;
    }
}
