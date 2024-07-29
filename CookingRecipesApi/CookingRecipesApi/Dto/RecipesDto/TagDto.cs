using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class TagDto
{
    [Required]
    public string Description { get; set; }
}
