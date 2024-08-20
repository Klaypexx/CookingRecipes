using Application.Auth;
using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Files.Services;
using Application.Foundation;
using Application.Recipes.Repositories;
using Application.Tags.Repositories;
using Infrastructure.Auth;
using Infrastructure.Auth.Repositories;
using Infrastructure.Auth.Utils;
using Infrastructure.Database;
using Infrastructure.Files.Services;
using Infrastructure.Foundation;
using Infrastructure.Recipes.Repositories;
using Infrastructure.Tags.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureBindings
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration )
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFileService, FileService>();

        string connectionString = configuration.GetConnectionString( "CookingRecipes" );
        services.AddDbContext<AppDbContext>( options => options.UseSqlServer( connectionString ) );
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
};