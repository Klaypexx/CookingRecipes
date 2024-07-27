using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions options ) : base( options ) { }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.ApplyConfigurationsFromAssembly( Assembly.GetExecutingAssembly() );
        }
    }
}
