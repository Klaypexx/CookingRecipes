namespace Application.Recipes.Entities;

public class OverviewRecipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public int CookingTime { get; set; }
    public int Portion { get; set; }
    public string AvatarPath { get; set; }
    public bool IsLike { get; set; }
    public List<Tag> Tags { get; set; }
}
