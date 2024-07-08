using Application.Foundation.Entities;
using Infrastructure.Foundation;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

// Add services to the container.

services.AddScoped<IUnitOfWork, UnitOfWork>();

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
