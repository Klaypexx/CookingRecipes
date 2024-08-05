using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class RecipeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int CookingTime { get; set; }
    [Required]
    public int Portion { get; set; }
    public IFormFile Avatar { get; set; }
    public List<TagDto> Tags { get; set; }
    [Required]
    public List<IngredientDto> Ingredients { get; set; }
    [Required]
    public List<StepDto> Steps { get; set; }
}
