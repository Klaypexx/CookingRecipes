using Domain.Recipes.Entities;

namespace Domain.Auth.Entities;
public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string AvatarPath { get; set; }
    public List<Recipe> Recipes { get; set; }
    public List<Recipe> Favourites { get; set; }
}
