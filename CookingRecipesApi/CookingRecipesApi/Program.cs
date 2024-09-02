using Application;
using Application.Auth;
using CookingRecipesApi;
using Infrastructure;
using Infrastructure.Auth;
using Infrastructure.Files;
using Infrastructure.Files.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder( args );

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

// Add services to the container.

services.AddApplication();

services.AddInfrastructure( configuration );

AuthSettings authSettings = configuration.GetSection( "Auth" ).Get<AuthSettings>();
services.AddScoped( sp => authSettings );

FileSettings fileSettings = configuration.GetSection( "Files" ).Get<FileSettings>();
services.AddScoped( sp => fileSettings );

string webRootPath = builder.Environment.WebRootPath;
services.AddSingleton( new WebHostSetting { WebRootPath = webRootPath } );

services.AddCookingRecipesApi();

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

string serverUrl = configuration[ "Cors:Url" ];

builder.Services.AddCors( options =>
{
    options.AddPolicy( "MyPolicy",
        policy =>
        {
            policy.WithOrigins( serverUrl )
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
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
