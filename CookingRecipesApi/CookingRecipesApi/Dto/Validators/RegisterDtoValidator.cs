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
            .WithMessage( "имя пользователя должно включать не менее 3 символов" )
            .MaximumLength( 25 )
            .WithMessage( "имя пользователя должно включать не более 25 символов" )
            .NotEmpty()
            .WithMessage( "имя пользователя не может быть пустым" );

        RuleFor( registerDto => registerDto.UserName )
            .MinimumLength( 3 )
            .WithMessage( "логин пользователя должен включать не менее 3 символов" )
            .MaximumLength( 25 )
            .WithMessage( "логин пользователя должен включать не более 25 символов" )
            .NotEmpty()
            .WithMessage( "логин пользователя не может быть пустым" );

        RuleFor( item => item )
            .MustAsync( async ( item, cancellation ) => await _authValidationRules.IsUniqueUsername( item.UserName ) )
            .WithMessage( item => "логин пользователя должен быть уникальным" );

        RuleFor( registerDto => registerDto.Password )
            .NotEmpty()
            .WithMessage( "пароль не может быть пустым" )
            .MinimumLength( 8 )
            .WithMessage( "пароль должен включать не менее 8 символов" )
            .MaximumLength( 25 )
            .WithMessage( "пароль должен включать не более 25 символов" );
    }
}
