using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class RecipeDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public int CookingTime { get; set; }
    public int Portion { get; set; }
    public string? AvatarPath { get; set; }
    public IFormFile Avatar { get; set; }
    public List<TagDto> Tags { get; set; }
    public List<IngredientDto> Ingredients { get; set; }
    public List<StepDto> Steps { get; set; }
}
