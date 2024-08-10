using Application.Auth.Repositories;
using Application.Auth.Services;
using Application.Foundation;
using Application.Recipes.Repositories;
using Application.Recipes.Services;
using Application.RecipesTags.Repositories;
using Application.RecipesTags.Services;
using Application.Tags.Repositories;
using Application.Tags.Services;
using Application.Users.Services;
using CookingRecipesApi.Auth;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.RecipesDto;
using FluentValidation;
using Infrastructure.Auth.Repositories;
using Infrastructure.Database;
using Infrastructure.Foundation;
using Infrastructure.Recipes.Repositories;
using Infrastructure.RecipesTags.Repository;
using Infrastructure.Tags.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder( args );

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

// Add services to the container.
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IRecipeService, RecipeService>();
services.AddScoped<ITagService, TagService>();
services.AddScoped<IRecipeTagService, RecipeTagService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<ITokenService, TokenService>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRecipeRepository, RecipeRepository>();
services.AddScoped<ITagRepository, TagRepository>();
services.AddScoped<IRecipeTagRepository, RecipeTagRepository>();

AuthSettings authSettings = configuration.GetSection( "Auth" ).Get<AuthSettings>();
services.AddScoped( sp => authSettings );

string connectionString = configuration.GetConnectionString( "CookingRecipes" );
services.AddDbContext<AppDbContext>( options => options.UseSqlServer( connectionString ) );

services.AddValidatorsFromAssemblyContaining<RegisterDto>();
services.AddValidatorsFromAssemblyContaining<LoginDto>();
services.AddValidatorsFromAssemblyContaining<RecipeDto>();
services.AddValidatorsFromAssemblyContaining<StepDto>();
services.AddValidatorsFromAssemblyContaining<IngredientDto>();

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
        .AddJwtBearer( options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = authSettings.Audience,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                IssuerSigningKey = TokenService.GetSymmetricSecurityKey( authSettings.Key ),
                ValidateIssuerSigningKey = true,

                RequireExpirationTime = true,
            };
        } );


builder.Services.AddCors( options =>
{
    options.AddPolicy( "MyPolicy",
        policy =>
        {
            policy.WithOrigins( "http://localhost:5173" ) // Укажите ваш фронтенд
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Разрешает использование учетных данных
        } );
} );

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors( "MyPolicy" );
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
