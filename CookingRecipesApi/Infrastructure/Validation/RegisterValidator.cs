using Application.Auth.Entities;
using Application.ResultObject;
using FluentValidation;
using FluentValidation.Results;

namespace CookingRecipesApi.Dto.Validators;

public class RegisterValidator : AbstractValidator<Register>, Application.Validation.IValidator<Register>
{
    private const int _nameMinWords = 3;
    private const int _nameMaxWords = 25;
    private const int _usernameMinWords = 3;
    private const int _usernameMaxWords = 25;
    private const int _passwordMinWords = 8;
    private const int _passwordMaxWords = 25;

    public RegisterValidator()
    {
        RuleFor( register => register.Name )
            .MinimumLength( _nameMinWords )
            .WithMessage( "Имя пользователя должно включать не менее " + _nameMinWords + " символов" )
            .MaximumLength( _nameMaxWords )
            .WithMessage( "Имя пользователя должно включать не более " + _nameMaxWords + " символов" )
            .NotEmpty()
            .WithMessage( "Имя пользователя не может быть пустым" );

        RuleFor( register => register.UserName )
            .MinimumLength( _usernameMinWords )
            .WithMessage( "Логин пользователя должен включать не менее " + _usernameMinWords + " символов" )
            .MaximumLength( _usernameMaxWords )
            .WithMessage( "Логин пользователя должен включать не более " + _usernameMaxWords + " символов" )
            .NotEmpty()
            .WithMessage( "Логин пользователя не может быть пустым" );

        RuleFor( register => register.Password )
            .NotEmpty()
            .WithMessage( "Пароль не может быть пустым" )
            .MinimumLength( _passwordMinWords )
            .WithMessage( "Пароль должен включать не менее " + _passwordMinWords + " символов" )
            .MaximumLength( _passwordMaxWords )
            .WithMessage( "Пароль должен включать не более " + _passwordMaxWords + " символов" );
    }

    Result Application.Validation.IValidator<Register>.Validate( Register entity )
    {
        ValidationResult result = Validate( entity );

        if ( result.IsValid )
        {
            return new Result();
        }

        return new Result( result.Errors.Select( x => new Error( x.ErrorMessage ) ).ToList() );
    }
}
