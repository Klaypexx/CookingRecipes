using Domain.Auth.Entities;

namespace Application.Auth.Repositories;

public interface IUserRepository
{
    Task AddUser( User user );
    Task<User> GetUserByUsername( string username );
    Task<User> GetUserByUsernameWithRecipes( string username );
    Task<User> GetUserByRefreshToken( string token );

}
