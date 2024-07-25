using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

public class RecipeTag
{
    public int RecipeId { get; set; }
    public int TagId { get; set; }
    public Recipe Recipe { get; set; }
    public Tag Tag { get; set; }
}
