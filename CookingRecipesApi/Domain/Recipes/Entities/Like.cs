using System.ComponentModel.DataAnnotations.Schema;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

[Table( "like" )]
public class Like
{
    [Column( "id_user" )]
    public string UserId { get; set; }
    [Column( "id_recipe" )]
    public string RecipeId { get; set; }
    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
