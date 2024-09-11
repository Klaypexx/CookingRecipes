using Application.ResultObject;
using Application.Users.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Infrastructure.Validation;

public class UserValidator : AbstractValidator<User>, Application.Validation.IValidator<User>
{
    private const int _nameMinWords = 3;
    private const int _nameMaxWords = 25;
    private const int _usernameMinWords = 3;
    private const int _usernameMaxWords = 25;
    private const int _descriptionMaxWords = 150;
    private const int _passwordMinWords = 8;
    private const int _passwordMaxWords = 25;

    public UserValidator()
    {
        RuleFor( user => user.Name )
            .MinimumLength( _nameMinWords )
            .WithMessage( "Имя пользователя должно включать не менее " + _nameMinWords + " символов" )
            .MaximumLength( _nameMaxWords )
            .WithMessage( "Имя пользователя должно включать не более " + _nameMaxWords + " символов" )
            .NotEmpty()
            .WithMessage( "Имя пользователя не может быть пустым" );

        RuleFor( user => user.UserName )
            .MinimumLength( _usernameMinWords )
            .WithMessage( "Логин пользователя должен включать не менее " + _usernameMinWords + " символов" )
            .MaximumLength( _usernameMaxWords )
            .WithMessage( "Логин пользователя должен включать не более " + _usernameMaxWords + " символов" )
            .NotEmpty()
            .WithMessage( "Логин пользователя не может быть пустым" );

        RuleFor( user => user.Description )
            .MaximumLength( _descriptionMaxWords )
            .WithMessage( "Описание пользователя должно включать не более " + _descriptionMaxWords + " символов" );

        RuleFor( user => user.Password )
            .MinimumLength( _passwordMinWords )
            .WithMessage( "Пароль должен включать не менее " + _passwordMinWords + "  символов" )
            .MaximumLength( _passwordMaxWords )
            .WithMessage( "Пароль должен включать не более " + _passwordMaxWords + " символов" );
    }

    Result Application.Validation.IValidator<User>.Validate( User entity )
    {
        ValidationResult result = Validate( entity );

        if ( result.IsValid )
        {
            return new Result();
        }

        List<Error> error = result.Errors.Select( x => new Error( x.ErrorMessage ) ).ToList();

        return new Result( error );
    }
}
