namespace Domain.Recipes.Entities;
public class Tag
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<RecipeTag> Recipes { get; set; }
}
