using System.ComponentModel.DataAnnotations;
using Domain.Recipes.Entities;

namespace CookingRecipesApi.Dto.RecipeDto;
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
    [Required]
    public string Avatar { get; set; }
    [Required]
    public List<TagDto> Tags { get; set; }
    [Required]
    public List<IngredientDto> Ingredients { get; set; }
    [Required]
    public List<StepDto> Steps { get; set; }
}
