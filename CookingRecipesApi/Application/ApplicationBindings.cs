using Application.Auth.Facade;
using Application.Auth.Services;
using Application.Favourites.Facade;
using Application.Favourites.Services;
using Application.Likes.Facade;
using Application.Likes.Services;
using Application.Recipes;
using Application.Recipes.Facade;
using Application.Recipes.Services;
using Application.Tags.Facade;
using Application.Tags.Services;
using Application.Users;
using Application.Users.Facade;
using Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationBindings
{
    public static IServiceCollection AddApplication( this IServiceCollection services )
    {
        services.AddScoped<IRecipeCreator, RecipeCreator>();
        services.AddScoped<IRecipeFacade, RecipeFacade>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<IAuthFacade, AuthFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<ITagFacade, TagFacade>();
        services.AddScoped<ILikeFacade, LikeFacade>();
        services.AddScoped<IFavouriteRecipeFacade, FavouriteRecipeFacade>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserCreator, UserCreator>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IFavouriteRecipeService, FavouriteRecipeService>();

        return services;
    }
}
