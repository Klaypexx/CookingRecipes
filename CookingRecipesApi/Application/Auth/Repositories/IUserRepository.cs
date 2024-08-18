using Domain.Auth.Entities;

namespace Application.Auth.Repositories;

public interface IUserRepository
{
    Task AddUser( User user );
    Task<User> GetByUsername( string username );
    Task<User> GetByRefreshToken( string token );

}
