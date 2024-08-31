using Application.Auth.Repositories;
using Domain.Auth.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Auth.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _entities;

    public UserRepository( AppDbContext context )
    {
        _entities = context.Set<User>();
    }
    public async Task AddUser( User user )
    {
        await _entities.AddAsync( user );
    }


    public async Task<User> GetUserByUsername( string username )
    {
        return await _entities
            .Where( u => u.UserName == username )
            .FirstOrDefaultAsync();
    }
    public async Task<User> GetUserByUsernameIncludingDependentEntities( string username )
    {
        return await _entities.Include( user => user.Recipes )
            .Include( user => user.Likes )
            .Include( user => user.FavouriteRecipes )
            .Where( u => u.UserName == username )
            .FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByRefreshToken( string token )
    {
        return await _entities
            .Where( u => u.RefreshToken == token )
            .FirstOrDefaultAsync();
    }
}
