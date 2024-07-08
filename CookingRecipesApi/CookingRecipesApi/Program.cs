using Application.Auth.Repositories;
using Application.Foundation.Entities;
using Application.Users.Entities;
using Application.Users.Services;
using Infrastructure;
using Infrastructure.Auth.Repositories;
using Infrastructure.Database;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

// Add services to the container.

services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IAuthService, AuthService>();

string connectionString = configuration.GetConnectionString("CookingRecipes");

services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
