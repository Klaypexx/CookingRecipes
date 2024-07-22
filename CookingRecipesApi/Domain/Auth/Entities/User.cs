using System.ComponentModel.DataAnnotations.Schema;
using Domain.Recipes.Entities;

namespace Domain.Auth.Entities;
public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public List<Like> Likes { get; set; }
    public List<Recipe> Recipes { get; set; }
    public List<FavouriteRecipe> FavouriteRecipes { get; set; }
    public void SetRefreshToken( string token, int expiration )
    {
        RefreshToken = token;
        RefreshTokenExpiryTime = DateTime.Now.AddMinutes( expiration );
    }
}
