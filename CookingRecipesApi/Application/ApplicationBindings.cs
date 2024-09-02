using Application.Auth.Services;
using Application.Favourites.Services;
using Application.Likes.Services;
using Application.Recipes;
using Application.Recipes.Services;
using Application.Tags.Services;
using Application.Users;
using Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationBindings
{
    public static IServiceCollection AddApplication( this IServiceCollection services )
    {
        services.AddScoped<IRecipeCreator, RecipeCreator>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserCreator, UserCreator>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IFavouriteRecipeService, FavouriteRecipeService>();

        return services;
    }
}
