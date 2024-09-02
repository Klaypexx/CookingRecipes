using Application.Users.Entities;

namespace Application.Users.Services;

public interface IUserService
{
    Task UpdateUser( User user, string userName );
    Task<UserInfo> GetUser( string userName );
    Task<UserStatistic> GetUserStatistic( string userName );
    Task<bool> IsUniqueUsername( string username );
}
