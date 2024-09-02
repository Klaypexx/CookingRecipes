using Application.Auth;
using Application.Auth.Entities;
using Application.Auth.Services;
using Application.Favourites.Repositories;
using Application.Files.Services;
using Application.Foundation;
using Application.Likes.Repositories;
using Application.Recipes.Entities;
using Application.Recipes.Repositories;
using Application.Tags.Repositories;
using Application.Users.Repositories;
using Application.Validation;
using CookingRecipesApi.Dto.Validators;
using Infrastructure.Auth;
using Infrastructure.Auth.Utils;
using Infrastructure.Database;
using Infrastructure.Favourites.Repositories;
using Infrastructure.Files.Services;
using Infrastructure.Foundation;
using Infrastructure.Likes.Repositories;
using Infrastructure.Recipes.Repositories;
using Infrastructure.Tags.Repository;
using Infrastructure.Users.Repositories;
using Infrastructure.Validation;
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
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IFavouriteRecipeRepository, FavouriteRecipeRepository>();

        services.AddScoped<IValidator<Register>, RegisterValidator>();
        services.AddScoped<IValidator<Login>, LoginValidator>();
        services.AddScoped<IValidator<Recipe>, RecipeValidator>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFileService, FileService>();

        string connectionString = configuration.GetConnectionString( "CookingRecipes" );
        services.AddDbContext<AppDbContext>( options => options.UseSqlServer( connectionString ) );
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
};