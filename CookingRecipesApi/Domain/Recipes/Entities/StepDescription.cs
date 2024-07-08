using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Recipes.Entities;
public class StepDescription
{
    public int Id { get; set; }
    public string Description { get; set; }
    public Ingredient Ingredient { get; set; }
}
