﻿namespace Domain.Recipes.Entities;

public class RecipeTag
{
    public int RecipeId { get; set; }
    public int TagId { get; set; }
    public Recipe Recipe { get; set; }
    public Tag Tag { get; set; }
}
