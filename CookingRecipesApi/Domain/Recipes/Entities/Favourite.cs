using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Recipes.Entities;
public class Favourite
{
    public string Id { get; set; }
    public int Count { get; set; }
    public Recipe Recipe { get; set; }
}
