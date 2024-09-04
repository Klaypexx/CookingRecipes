using Application.ResultObject;
using Application.Users.Entities;

namespace Application.Users.Services;

public interface IUserService
{
    Task<Result> UpdateUser( User user, string userName );
    Task<Result<UserInfo>> GetUser( string userName );
    Task<Result<UserStatistic>> GetUserStatistic( string userName );
    Task<bool> IsUniqueUsername( string username );
}
