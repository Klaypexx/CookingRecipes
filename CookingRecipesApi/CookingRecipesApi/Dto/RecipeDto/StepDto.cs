using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipeDto;
public class StepDto
{
    [Required]
    public string Description { get; set; }
}
