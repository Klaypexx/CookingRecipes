﻿using Application.Auth.Entities;
using Application.Auth.Services;
using Application.Foundation;
using Application.ResultObject;
using Application.Validation;

namespace Application.Auth.Facade;

public class AuthFacade : IAuthFacade
{
    private readonly IAuthService _authService;
    private readonly IValidator<Register> _registerValidator;
    private readonly IValidator<Login> _loginValidator;
    private readonly IUnitOfWork _unitOfWork;

    public AuthFacade( IAuthService authService,
        IValidator<Register> registerValidator,
        IValidator<Login> loginValidator,
        IUnitOfWork unitOfWork )
    {
        _authService = authService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> RegisterUser( Register register )
    {
        try
        {
            Result result = _registerValidator.Validate( register );

            if ( !result.IsSuccess )
            {
                return result;
            }

            await _authService.RegisterUser( register );

            await _unitOfWork.Save();

            return new Result();

        }
        catch ( Exception e )
        {
            return new Result( new Error( e.Message ) );
        }
    }

    public async Task<Result<AuthTokenSet>> SignIn( Login login, int lifeTime )
    {
        try
        {
            Result result = _loginValidator.Validate( login );

            if ( !result.IsSuccess )
            {
                return new Result<AuthTokenSet>( result.Errors );
            }

            AuthTokenSet tokens = await _authService.SignIn( login, lifeTime );

            await _unitOfWork.Save();

            return new Result<AuthTokenSet>( tokens );
        }
        catch ( Exception e )
        {
            return new Result<AuthTokenSet>( new Error( e.Message ) );
        }
    }

    public async Task<Result<AuthTokenSet>> Refresh( string cookieRefreshToken, int lifetime )
    {
        try
        {
            AuthTokenSet tokens = await _authService.Refresh( cookieRefreshToken, lifetime );

            await _unitOfWork.Save();

            return new Result<AuthTokenSet>( tokens );
        }
        catch ( Exception e )
        {
            return new Result<AuthTokenSet>( new Error( e.Message ) );
        }
    }
}
