using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.AuthDto;
public class TokenDto
{
    [Required]
    public string AccessToken { get; set; }
}

