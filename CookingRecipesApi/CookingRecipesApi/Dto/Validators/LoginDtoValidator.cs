using CookingRecipesApi.Dto.AuthDto;
using FluentValidation;

namespace CookingRecipesApi.Dto.Validators;
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor( loginDto => loginDto.UserName )
            .MinimumLength( 3 )
            .WithMessage( "Логин должен включать не менее 3 символов" )
            .MaximumLength( 25 )
            .WithMessage( "Логин должен включать не более 25 символов" )
            .NotEmpty()
            .WithMessage( "Логин не может быть пустым" );

        RuleFor( loginDto => loginDto.Password )
            .NotEmpty()
            .WithMessage( "Пароль не может быть пустым" )
            .MinimumLength( 8 )
            .WithMessage( "Пароль должен включать не менее 8 символов" )
            .MaximumLength( 25 )
            .WithMessage( "Пароль должен включать не более 25 символов" );
    }
}
