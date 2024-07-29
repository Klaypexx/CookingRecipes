using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;
using Domain.Recipes.Entities;

namespace Application.Recipes.Services;
public interface IRecipeService
{
    Task CreateRcipe( Recipe recipe );
    Task<List<RecipeTag>> GetOrCreateTag( List<string> tagNames );
    Task<List<Recipe>> GetAllUserRecipes( int userId );
}
