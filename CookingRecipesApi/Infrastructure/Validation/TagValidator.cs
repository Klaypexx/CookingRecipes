using Application.Recipes.Entities;
using FluentValidation;

namespace Infrastructure.Validation;

public class TagValidator : AbstractValidator<Tag>
{
    private const int _tagMaxWords = 20;

    public TagValidator()
    {
        RuleFor( s => s.Name )
            .MaximumLength( _tagMaxWords ).WithMessage( "Тег не должен превышать" + _tagMaxWords + "символов" );
    }
}
