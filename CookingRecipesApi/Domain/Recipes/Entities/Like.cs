namespace Domain.Recipes.Entities;
public class Like
{
    public string Id { get; set; }
    public int Count { get; set; }
    public Recipe Recipe { get; set; }
}
