using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Migrations;

class HotelManagementDbContextFactory : IDesignTimeDbContextFactory<HotelManagementDbContext>
{
    public HotelManagementDbContext CreateDbContext(string[] args)
    {
        //IConfiguration config = GetConfig();
        string connectionString = "Server=SANCHEZ_\\SQLEXPRESS;Database=HotelManagement;Trusted_Connection=true;TrustServerCertificate=True;";
        var optionalBuilder = new DbContextOptionsBuilder<HotelManagementDbContext>();

        optionalBuilder.UseSqlServer(connectionString,
            ob => ob.MigrationsAssembly("Infrastructure.Migrations"));

        return new HotelManagementDbContext(optionalBuilder.Options);
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
