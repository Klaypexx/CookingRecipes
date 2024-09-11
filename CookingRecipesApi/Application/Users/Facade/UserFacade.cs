using Application.ResultObject;
using Application.Users.Entities;
using Application.Users.Services;
using Application.Validation;

namespace Application.Users.Facade;

public class UserFacade : IUserFacade
{
    private readonly IUserService _userService;
    private readonly IValidator<User> _userValidator;

    public UserFacade( IUserService userService,
        IValidator<User> userValidator )
    {
        _userService = userService;
        _userValidator = userValidator;
    }

    public async Task<Result> UpdateUser( User user, string userName )
    {
        try
        {
            Result result = _userValidator.Validate( user );

            if ( !result.IsSuccess )
            {
                return result;
            }

            await _userService.UpdateUser( user, userName );

            return new Result();
        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result<UserInfo>> GetUser( string userName )
    {
        try
        {
            UserInfo user = await _userService.GetUser( userName );

            return new Result<UserInfo>( user );
        }
        catch ( Exception e )
        {
            return new Result<UserInfo>( new Error( e.Message ) );
        }
    }

    public async Task<Result<UserStatistic>> GetUserStatistic( string userName )
    {
        try
        {
            UserStatistic userStatistic = await _userService.GetUserStatistic( userName );

            return new Result<UserStatistic>( userStatistic );
        }
        catch ( Exception e )
        {
            return new Result<UserStatistic>( new Error( e.Message ) );
        }
    }
}
