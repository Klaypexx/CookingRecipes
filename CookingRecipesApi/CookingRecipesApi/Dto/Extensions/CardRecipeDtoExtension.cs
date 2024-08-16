using CookingRecipesApi.Dto.RecipesDto;
using Domain.Recipes.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CookingRecipesApi.Dto.Extensions
{
    public static class CardRecipeDtoExtension
    {
        public static CardRecipeDto ToCardRecipeDto( this Recipe recipe )
        {
            return new CardRecipeDto
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

        public static IReadOnlyList<CardRecipeDto> ToCardRecipeDto( this IReadOnlyList<Recipe> recipes )
        {
            return recipes.Select( recipe => recipe.ToCardRecipeDto() ).ToList();
        }
    }
}