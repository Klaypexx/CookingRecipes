using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Recipes.Entities;

namespace Domain.Auth.Entities;
public class User
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public int Password { get; set; }

    public string AvatarPath {  get; set; }

    public List<Recipe> Recipes { get; set; }

}
