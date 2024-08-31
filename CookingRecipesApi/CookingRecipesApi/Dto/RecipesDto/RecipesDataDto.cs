namespace CookingRecipesApi.Dto.RecipesDto;

public class RecipesDataDto<T>
{
    public IReadOnlyList<T> Recipes { get; set; }
    public bool IsLastRecipes { get; set; }

    public RecipesDataDto( IReadOnlyList<T> recipes, bool isLastRecipes )
    {
        Recipes = recipes;
        IsLastRecipes = isLastRecipes;
    }
}
