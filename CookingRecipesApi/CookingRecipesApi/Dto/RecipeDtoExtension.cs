using CookingRecipesApi.Dto.RecipesDto;
using Domain.Recipes.Entities;

namespace CookingRecipesApi.Dto;
public static class RecipeDtoExtension
{
    public static Recipe ToDomain( this RecipeDto recipeDto, int authorId, List<RecipeTag> Tags )
    {
        return new()
        {
            Name = recipeDto.Name,
            Description = recipeDto.Description,
            Avatar = recipeDto.Avatar?.FileName,
            CookingTime = recipeDto.CookingTime,
            Portion = recipeDto.Portion,
            AuthorId = authorId,
            Ingredients = recipeDto.Ingredients.Select( ingredientDto => new Ingredient
            {
                Name = ingredientDto.Name,
                Product = ingredientDto.Product
            } ).ToList(),
            Steps = recipeDto.Steps.Select( stepDto => new Step
            {
                Description = stepDto.Description,
            } ).ToList(),
            Tags = Tags
        };
    }
    public static CardRecipeDto ToDto( this Recipe recipe )
    {
        return new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Portion = recipe.Portion,
            AvatarPath = recipe.Avatar,
            AuthorName = recipe.Author.UserName,
            Tags = recipe.Tags.Select( recipeTag => new TagDto
            {
                Name = recipeTag.Tag.Name
            } ).ToList()

        };
    }
}
