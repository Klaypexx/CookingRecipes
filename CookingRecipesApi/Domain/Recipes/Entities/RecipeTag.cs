using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

[Table( "recipe_tag" )]
public class RecipeTag
{
    [Column( "id_recipe" )]
    public string RecipeId { get; set; }
    [Column( "id_tag" )]
    public string TagId { get; set; }
    public Recipe Recipe { get; set; }
    public Tag Tag { get; set; }
}
