using Application.ResultObject;
using Application.Users.Entities;

namespace Application.Users.Facade;

public interface IUserFacade
{
    Task<Result> UpdateUser( User user, string userName );
    Task<Result<UserInfo>> GetUser( string userName );
    Task<Result<UserStatistic>> GetUserStatistic( string userName );
}
