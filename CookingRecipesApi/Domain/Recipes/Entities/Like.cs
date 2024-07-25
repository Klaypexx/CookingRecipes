using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

public class Like
{
    /* Like()
     {
         List<Recipe> recipes = new List<Recipe>() { new(), new(), new() };


         var user = new User();


         IEnumerable<string> userRecipeIds = user.Likes
             .Select( z => z.Recipe )
             .Select( z => z.Id );

         List<Recipe> qqq = user.Likes
             .Select( x => recipes.IntersectBy( userRecipeIds, y => y.Id ) )
             .SelectMany( x => x )
             .ToList();
     }*/

    public string UserId { get; set; }
    public string RecipeId { get; set; }
    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
