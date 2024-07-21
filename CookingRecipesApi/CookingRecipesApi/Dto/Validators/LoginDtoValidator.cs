using CookingRecipesApi.Dto.AuthDto;
using FluentValidation;

namespace CookingRecipesApi.Dto.Validators;
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor( loginDto => loginDto.UserName )
            .MinimumLength( 3 )
            .WithMessage( "логин должен включать не менее 3 символов" )
            .MaximumLength( 25 )
            .WithMessage( "логин должен включать не более 25 символов" )
            .NotEmpty()
            .WithMessage( "логин не может быть пустым" );

        RuleFor( loginDto => loginDto.Password )
            .NotEmpty()
            .WithMessage( "пароль не может быть пустым" )
            .MinimumLength( 8 )
            .WithMessage( "пароль должен включать не менее 8 символов" )
            .MaximumLength( 25 )
            .WithMessage( "пароль должен включать не более 25 символов" );
    }
}
