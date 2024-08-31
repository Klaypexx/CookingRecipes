namespace Application.Recipes.Entities;

public class RecipesData<T>
{
    public IReadOnlyList<T> Recipes { get; set; }
    public bool IsLastRecipes { get; set; }

    public RecipesData( IReadOnlyList<T> recipes, bool isLastRecipes )
    {
        Recipes = recipes;
        IsLastRecipes = isLastRecipes;
    }
}
