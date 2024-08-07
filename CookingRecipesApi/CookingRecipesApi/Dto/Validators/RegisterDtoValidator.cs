using Application.Auth.Repositories;
using Application.Validation;
using CookingRecipesApi.Dto.AuthDto;
using FluentValidation;

namespace CookingRecipesApi.Dto.Validators;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    private readonly AuthValidationRules _authValidationRules;

    private const int _nameMinWords = 3;
    private const int _nameMaxWords = 25;
    private const int _usernameMinWords = 3;
    private const int _usernameMaxWords = 25;
    private const int _passwordMinWords = 8;
    private const int _passwordMaxWords = 25;

    public RegisterDtoValidator( IUserRepository userRepository )
    {
        _authValidationRules = new AuthValidationRules( userRepository );

        RuleFor( registerDto => registerDto.Name )
            .MinimumLength( _nameMinWords )
            .WithMessage( "Имя пользователя должно включать не менее" + _nameMinWords + "символов" )
            .MaximumLength( _nameMaxWords )
            .WithMessage( "Имя пользователя должно включать не более" + _nameMaxWords + "символов" )
            .NotEmpty()
            .WithMessage( "Имя пользователя не может быть пустым" );

        RuleFor( registerDto => registerDto.UserName )
            .MinimumLength( _usernameMinWords )
            .WithMessage( "Логин пользователя должен включать не менее" + _usernameMinWords + "символов" )
            .MaximumLength( _usernameMaxWords )
            .WithMessage( "Логин пользователя должен включать не более" + _usernameMaxWords + "символов" )
            .NotEmpty()
            .WithMessage( "Логин пользователя не может быть пустым" );

        RuleFor( item => item )
            .MustAsync( async ( item, cancellation ) => await _authValidationRules.IsUniqueUsername( item.UserName ) )
            .WithMessage( item => "Логин пользователя должен быть уникальным" );

        RuleFor( registerDto => registerDto.Password )
            .NotEmpty()
            .WithMessage( "Пароль не может быть пустым" )
            .MinimumLength( _passwordMinWords )
            .WithMessage( "Пароль должен включать не менее" + _passwordMinWords + "символов" )
            .MaximumLength( _passwordMaxWords )
            .WithMessage( "Пароль должен включать не более" + _passwordMaxWords + "символов" );
    }
}
