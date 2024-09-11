using Application.Recipes.Entities;
using FluentValidation;

namespace Infrastructure.Validation;

public class StepValidator : AbstractValidator<Step>
{
    private const int _stepsDescriptionMaxWords = 300;

    public StepValidator()
    {
        RuleFor( s => s.Description )
            .NotEmpty().WithMessage( "Описание шага обязательно" )
            .MaximumLength( _stepsDescriptionMaxWords ).WithMessage( "Описание шага не должно превышать" + _stepsDescriptionMaxWords + "символов" );
    }
}
