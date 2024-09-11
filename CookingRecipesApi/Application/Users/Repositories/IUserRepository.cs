using Domain.Auth.Entities;

namespace Application.Users.Repositories;

public interface IUserRepository
{
    Task AddUser( User user );
    Task<User> GetUserByUsername( string username );
    Task<User> GetUserByUsernameWithDetails( string username );
    Task<User> GetUserByRefreshToken( string token );

}
