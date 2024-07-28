﻿using System.ComponentModel.DataAnnotations;

namespace CookingRecipesApi.Dto.RecipeDto;
public class IngredientDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Product { get; set; }
}
