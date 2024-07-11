using Application.Auth.Entities;
using Application.Auth.Repositories;
using Application.Foundation.Entities;
using Application.Users.Entities;
using Application.Users.Services;
using CookingRecipesApi.Auth;
using Infrastructure.Auth.Repositories;
using Infrastructure.Auth.Utils;
using Infrastructure.Database;
using Infrastructure.Foundation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder( args );

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

// Add services to the container.
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<ITokenService, TokenService>();

services.AddScoped<IUserRepository, UserRepository>();

AuthSettings authSettings = configuration.GetSection( "Auth" ).Get<AuthSettings>();
services.AddScoped( sp => authSettings );

string connectionString = configuration.GetConnectionString( "CookingRecipes" );
services.AddDbContext<AppDbContext>( options => options.UseSqlServer( connectionString ) );

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            // Токен в куки
            /*options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[ "jwt_token" ];
                    return Task.CompletedTask;
                }
            };*/
        } );

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
