using Application.Auth.Repositories;
using Application.Validation;
using CookingRecipesApi.Dto.AuthDto;
using FluentValidation;

namespace CookingRecipesApi.Dto.Validators;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    private readonly AuthValidationRules _authValidationRules;

    public RegisterDtoValidator( IUserRepository userRepository )
    {
        _authValidationRules = new AuthValidationRules( userRepository );

        RuleFor( registerDto => registerDto.Name )
            .MinimumLength( 3 )
            .WithMessage( "Имя пользователя должно включать не менее 3 символов" )
            .MaximumLength( 25 )
            .WithMessage( "Имя пользователя должно включать не более 25 символов" )
            .NotEmpty()
            .WithMessage( "Имя пользователя не может быть пустым" );

        RuleFor( registerDto => registerDto.UserName )
            .MinimumLength( 3 )
            .WithMessage( "Логин пользователя должен включать не менее 3 символов" )
            .MaximumLength( 25 )
            .WithMessage( "Логин пользователя должен включать не более 25 символов" )
            .NotEmpty()
            .WithMessage( "Логин пользователя не может быть пустым" );

        RuleFor( item => item )
            .MustAsync( async ( item, cancellation ) => await _authValidationRules.IsUniqueUsername( item.UserName ) )
            .WithMessage( item => "Логин пользователя должен быть уникальным" );

        RuleFor( registerDto => registerDto.Password )
            .NotEmpty()
            .WithMessage( "Пароль не может быть пустым" )
            .MinimumLength( 8 )
            .WithMessage( "Пароль должен включать не менее 8 символов" )
            .MaximumLength( 25 )
            .WithMessage( "Пароль должен включать не более 25 символов" );
    }
}
