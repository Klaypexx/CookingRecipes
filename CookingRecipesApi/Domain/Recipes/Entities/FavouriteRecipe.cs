using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;
public class FavouriteRecipe
{
    public string UserId { get; set; }
    public string RecipeId { get; set; }
    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
