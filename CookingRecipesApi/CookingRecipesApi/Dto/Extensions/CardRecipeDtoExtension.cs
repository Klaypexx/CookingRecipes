using CookingRecipesApi.Dto.RecipesDto;
using Domain.Recipes.Entities;

namespace CookingRecipesApi.Dto.Extensions;

public static class CardRecipeDtoExtension
{
    public static CardRecipeDto ToCardRecipeDto( this Recipe recipe )
    {
        return new()
        {
            Id = recipe.Id,
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
