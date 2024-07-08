using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddDatabaseFoundations(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("CookingRecipes");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }
}
