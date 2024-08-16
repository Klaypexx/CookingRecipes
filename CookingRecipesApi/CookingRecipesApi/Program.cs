using Application.Recipes;
using Infrastructure.Auth;
using Application.Auth;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.RecipesDto;
using FluentValidation;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder( args );

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

// Add services to the container.

services.AddApplicationServices();

services.AddInfrastructureRepositories()
    .AddInfrastructureServices()
    .AddInfrastructureDatabase( configuration );

AuthSettings authSettings = configuration.GetSection( "Auth" ).Get<AuthSettings>();
services.AddScoped( sp => authSettings );

string webRootPath = builder.Environment.WebRootPath;
services.AddSingleton( new WebHostSetting { WebRootPath = webRootPath } );

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
