using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class CardRecipeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int CookingTime { get; set; }
    [Required]
    public int Portion { get; set; }
    public string AvatarPath { get; set; }
    public List<TagDto> Tags { get; set; }
}
