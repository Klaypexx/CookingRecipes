using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Auth.Utils;
using Application.Foundation;
using Application.Recipes.Repositories;
using Application.Tags.Repositories;
using Infrastructure.Auth;
using Infrastructure.Auth.Repositories;
using Infrastructure.Auth.Utils;
using Infrastructure.Foundation;
using Infrastructure.Recipes.Repositories;
using Infrastructure.Tags.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class InfrastructureBindings
{
    public static IServiceCollection AddRepositories( this IServiceCollection services )
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        return services;
    }
    public static IServiceCollection AddDatabase( this IServiceCollection services )
    {
        // тут настройки di
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
};