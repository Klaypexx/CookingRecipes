using Application.Auth.Entities;
using Application.ResultObject;
using FluentValidation;
using FluentValidation.Results;

namespace CookingRecipesApi.Dto.Validators;

public class LoginValidator : AbstractValidator<Login>, Application.Validation.IValidator<Login>
{
    private const int _usernameMinWords = 3;
    private const int _usernameMaxWords = 25;
    private const int _passwordMinWords = 8;
    private const int _passwordMaxWords = 25;
    public LoginValidator()
    {
        RuleFor( login => login.UserName )
            .MinimumLength( _usernameMinWords )
            .WithMessage( "Логин должен включать не менее " + _usernameMinWords + " символов" )
            .MaximumLength( _usernameMaxWords )
            .WithMessage( "Логин должен включать не более " + _usernameMaxWords + " символов" )
            .NotEmpty()
            .WithMessage( "Логин не может быть пустым" );

        RuleFor( login => login.Password )
            .NotEmpty()
            .WithMessage( "Пароль не может быть пустым" )
            .MinimumLength( _passwordMinWords )
            .WithMessage( "Пароль должен включать не менее " + _passwordMinWords + " символов" )
            .MaximumLength( _passwordMaxWords )
            .WithMessage( "Пароль должен включать не более " + _passwordMaxWords + " символов" );
    }

    Result Application.Validation.IValidator<Login>.Validate( Login entity )
    {
        ValidationResult result = Validate( entity );

        if ( result.IsValid )
        {
            return new Result();
        }

        return new Result( result.Errors.Select( x => new Error( x.ErrorMessage ) ).ToList() );
    }
}
