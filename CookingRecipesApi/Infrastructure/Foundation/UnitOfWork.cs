using Application.Foundation.Entities;
using Infrastructure.Database;

namespace Infrastructure.Foundation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}