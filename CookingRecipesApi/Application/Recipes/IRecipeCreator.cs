using Domain.Recipes.Entities;

namespace Application.Recipes;
public interface IRecipeCreator
{
    Recipe Create( Entities.Recipe recipe, string avatarGuid );
}
