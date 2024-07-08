using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;
public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public List<Tag> Tags { get; set; }
    public DateTime CookingTime { get; set; }
    public int Portion { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<StepDescription> StepDescriptions { get; set; }
    public int Like { get; set; }
    public User User { get; set; }

}
