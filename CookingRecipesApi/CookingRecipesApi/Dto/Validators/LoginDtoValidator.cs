using CookingRecipesApi.Dto.AuthDto;
using FluentValidation;

namespace CookingRecipesApi.Dto.Validators;
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    private const int _usernameMinWords = 3;
    private const int _usernameMaxWords = 25;
    private const int _passwordMinWords = 8;
    private const int _passwordMaxWords = 25;
    public LoginDtoValidator()
    {

        RuleFor( loginDto => loginDto.UserName )
            .MinimumLength( _usernameMinWords )
            .WithMessage( "Логин должен включать не менее" + _usernameMinWords + "символов" )
            .MaximumLength( _usernameMaxWords )
            .WithMessage( "Логин должен включать не более" + _usernameMaxWords + "символов" )
            .NotEmpty()
            .WithMessage( "Логин не может быть пустым" );

        RuleFor( loginDto => loginDto.Password )
            .NotEmpty()
            .WithMessage( "Пароль не может быть пустым" )
            .MinimumLength( _passwordMinWords )
            .WithMessage( "Пароль должен включать не менее" + _passwordMinWords + "символов" )
            .MaximumLength( _passwordMaxWords )
            .WithMessage( "Пароль должен включать не более" + _passwordMaxWords + "символов" );
    }
}
