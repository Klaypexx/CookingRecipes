using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class CurrentRecipeDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CookingTime { get; set; }
    public int Portion { get; set; }
    public string AuthorName { get; set; }
    public List<IngredientDto> Ingredients { get; set; }
    public List<StepDto> Steps { get; set; }
    public string AvatarPath { get; set; }
    public List<TagDto> Tags { get; set; }
}
