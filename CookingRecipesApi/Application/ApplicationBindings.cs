using Application.Auth.Services;
using Application.Recipes.Services;
using Application.Recipes;
using Application.Tags.Services;
using Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationBindings
{
    public static IServiceCollection AddApplicationServices( this IServiceCollection services )
    {
        services.AddScoped<IRecipeCreator, RecipeCreator>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<ITagService, TagService>();

        return services;
    }
}
