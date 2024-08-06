﻿using CookingRecipesApi.Dto.RecipesDto;
using Domain.Recipes.Entities;

namespace CookingRecipesApi.Dto.Extensions;
public static class RecipeDtoExtension
{
    public static Recipe ToDomain( this RecipeDto recipeDto, int authorId, List<RecipeTag> Tags, string avatarGuid )
    {
        return new()
        {
            Name = recipeDto.Name,
            Description = recipeDto.Description,
            Avatar = avatarGuid,
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
}
