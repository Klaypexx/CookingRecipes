using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Recipes.Entities;

namespace Application.RecipesTags.Repositories;
public interface IRecipeTagRepository
{
    Task<List<RecipeTag>> GetRecipeTagsByRecipeIdAndTagIds( int recipeId, List<int> tagsId );
    void RemoveConnections( List<RecipeTag> recipeTag );
    Task CreateConnections( List<RecipeTag> recipeTag );
}
