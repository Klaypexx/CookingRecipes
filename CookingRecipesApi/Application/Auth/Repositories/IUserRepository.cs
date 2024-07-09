using Domain.Auth.Entities;


namespace Application.Auth.Repositories;
public interface IUserRepository
{
    Task AddUser( User user );
    Task<User> GetUser( int userId );
    Task<User> GetByUsername( string username );

}
