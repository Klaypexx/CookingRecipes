using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.AuthDto;
public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; }
}
