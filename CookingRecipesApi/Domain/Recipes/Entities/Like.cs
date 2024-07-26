﻿using Domain.Auth.Entities;

namespace Domain.Recipes.Entities;

public class Like
{
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
