using Domain.Recipes.Entities;

namespace Domain.Auth.Entities;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string UserName { get; private set; }
    public string Description { get; private set; }
    public string Password { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiryTime { get; private set; }
    public List<Like> Likes { get; private set; }
    public List<Recipe> Recipes { get; private set; }
    public List<FavouriteRecipe> FavouriteRecipes { get; private set; }

    public User() { }

    public User( string name, string username, string password )
    {
        Name = name;
        UserName = username;
        Password = password;
    }

    public void SetPassword( string password )
    {
        Password = password;
    }

    public void SetRefreshToken( string token, int expiration )
    {
        RefreshToken = token;
        RefreshTokenExpiryTime = DateTime.Now.AddDays( expiration );
    }
}
