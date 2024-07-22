using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Recipes.Entities;
public class Step
{
    public string Id { get; set; }
    public int StepNumber { get; set; }
    public string Description { get; set; }
    [Column( "id_recipe" )]
    public string RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
