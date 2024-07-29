using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class StepDto
{
    [Required]
    public string Description { get; set; }
}
