using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipeDto;
public class TagDto
{
    [Required]
    public string Description { get; set; }
}
