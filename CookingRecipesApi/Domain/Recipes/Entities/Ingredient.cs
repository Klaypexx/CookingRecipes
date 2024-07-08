using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Recipes.Entities;
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Product { get; set; }
    public Recipe Recipe { get; set; }
}
