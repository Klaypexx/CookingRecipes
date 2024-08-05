using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipesDto;
public class CardRecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public int CookingTime { get; set; }
    public int Portion { get; set; }
    public string AvatarPath { get; set; }
    public List<TagDto> Tags { get; set; }
}
