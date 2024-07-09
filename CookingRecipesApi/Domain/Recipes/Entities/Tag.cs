namespace Domain.Recipes.Entities;
public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Recipe> Recipes { get; set; }
}
