using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class CurrentRecipeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int CookingTime { get; set; }
    [Required]
    public int Portion { get; set; }
    [Required]
    public string AuthorName { get; set; }
    [Required]
    public List<IngredientDto> Ingredients { get; set; }
    [Required]
    public List<StepDto> Steps { get; set; }
    public string AvatarPath { get; set; }
    public List<TagDto> Tags { get; set; }
}
