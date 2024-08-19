using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

public class Recipe
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int CookingTime { get; private set; }
    public int Portion { get; private set; }
    public string Avatar { get; private set; }
    public int AuthorId { get; private set; }
    public List<Like> Likes { get; private set; }
    public List<RecipeTag> Tags { get; private set; }
    public List<Ingredient> Ingredients { get; private set; }
    public List<Step> Steps { get; private set; }
    public User Author { get; private set; }
    public List<FavouriteRecipe> FavouriteRecipes { get; private set; }

    public Recipe() { }

    public Recipe( string name,
        string description,
        int cookingTime,
        int portion,
        string avatar,
        int authorId,
        List<Ingredient> ingredients,
        List<Step> steps,
        List<RecipeTag> tags )
    {
        Name = name;
        Description = description;
        CookingTime = cookingTime;
        Portion = portion;
        Avatar = avatar;
        AuthorId = authorId;
        Ingredients = ingredients;
        Steps = steps;
        Tags = tags;
    }

    public void SetTags( List<RecipeTag> tags )
    {
        Tags = tags;
    }

    public void UpdateRecipe( Recipe otherRecipe )
    {
        Name = otherRecipe.Name;
        Description = otherRecipe.Description;
        CookingTime = otherRecipe.CookingTime;
        Portion = otherRecipe.Portion;
        Avatar = otherRecipe.Avatar;
        Ingredients = otherRecipe.Ingredients;
        Steps = otherRecipe.Steps;
        Tags = otherRecipe.Tags;
    }
}
