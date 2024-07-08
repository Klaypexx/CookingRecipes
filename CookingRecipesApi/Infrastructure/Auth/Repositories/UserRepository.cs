using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.Repositories;
using Domain.Auth.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Auth.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _entities;

    public UserRepository(AppDbContext context)
    {
        _entities = context.Set<User>();
    }
    public async Task AddUser(User user)
    {
        await _entities.AddAsync(user);
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _entities
            .Where(u => u.UserName == username)
            .FirstOrDefaultAsync();
    }

    public async Task<User> GetUser(int userId)
    {
        return await _entities.FindAsync(userId);
    }
}
