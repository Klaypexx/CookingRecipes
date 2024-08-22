namespace Application.Recipes.Entities;

public class CompleteRecipe
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CookingTime { get; set; }
    public int Portion { get; set; }
    public string AuthorName { get; set; }
    public string AvatarPath { get; set; }
    public bool IsLike { get; set; }
    public int LikeCount { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Step> Steps { get; set; }
    public List<Tag> Tags { get; set; }
}
