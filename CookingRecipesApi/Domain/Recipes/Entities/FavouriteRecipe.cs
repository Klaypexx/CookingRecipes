using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

[Table( "favourite_recipe" )]
public class FavouriteRecipe
{
    [Column( "id_user" )]
    public string UserId { get; set; }
    [Column( "id_recipe" )]
    public string RecipeId { get; set; }
    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
