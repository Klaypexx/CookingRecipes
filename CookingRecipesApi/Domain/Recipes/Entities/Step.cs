namespace Domain.Recipes.Entities;
public class Step
{
    public string Id { get; set; }
    public int StepNumber { get; set; }
    public string Description { get; set; }
    public Recipe Recipe { get; set; }
}
