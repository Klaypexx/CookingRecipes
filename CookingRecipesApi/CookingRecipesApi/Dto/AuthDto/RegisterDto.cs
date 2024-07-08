using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.AuthDto;

public class RegisterDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
