using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Migrations;

class HotelManagementDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        //IConfiguration config = GetConfig();
        string connectionString = "Server=SANCHEZ_\\SQLEXPRESS;Database=CookingRecipes;Trusted_Connection=true;TrustServerCertificate=True;";
        var optionalBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionalBuilder.UseSqlServer(connectionString,
            ob => ob.MigrationsAssembly("Infrastructure.Migrations"));

        return new AppDbContext(optionalBuilder.Options);
    }

    /*private static IConfiguration GetConfig()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);

        return builder.Build();
    }*/
}
