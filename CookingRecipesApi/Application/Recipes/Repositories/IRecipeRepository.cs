using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;
using Domain.Recipes.Entities;

namespace Application.Recipes.Repositories;
public interface IRecipeRepository
{
    Task CreateRecipe( Recipe recipe );
    Task<List<Recipe>> GetAllUserRecipes( int userId );
}
