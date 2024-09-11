using Application.Recipes.Entities;
using Application.ResultObject;
using Application.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Infrastructure.Validation;

public class RecipeValidator : AbstractValidator<Recipe>, Application.Validation.IValidator<Recipe>
{
    private readonly FileValidationRule _fileValidationRules;

    private const int _nameMaxWords = 50;
    private const int _descriptionMaxWords = 150;

    public RecipeValidator()
    {
        _fileValidationRules = new FileValidationRule();

        RuleFor( r => r.Name )
            .NotEmpty().WithMessage( "Название рецепта обязательно" )
            .MaximumLength( _nameMaxWords ).WithMessage( "Название не должно превышать" + _nameMaxWords + "символов" );

        RuleFor( r => r.Description )
            .NotEmpty().WithMessage( "Описание рецепта обязательно" )
            .MaximumLength( _descriptionMaxWords ).WithMessage( "Описание не должно превышать" + _descriptionMaxWords + "символов" );

        RuleFor( item => item )
            .Must( ( item, cancellation ) => _fileValidationRules.IsImage( item.Avatar ) )
            .When( r => r.Avatar != null )
            .WithMessage( "Неподдерживаемый формат файла" );

        RuleFor( r => r.CookingTime )
            .NotEmpty().WithMessage( "Время приготовления обязательно" );

        RuleFor( r => r.Portion )
            .NotEmpty().WithMessage( "Количество порций обязательно" );

        RuleForEach( r => r.Tags )
           .SetValidator( new TagValidator() );

        RuleForEach( r => r.Steps )
            .NotEmpty().WithMessage( "Поле шагов не должно быть пустым" )
            .SetValidator( new StepValidator() );

        RuleForEach( r => r.Ingredients )
            .NotEmpty().WithMessage( "Поле ингредиентов не должно быть пустым" )
            .SetValidator( new IngredientValidator() );
    }

    Result Application.Validation.IValidator<Recipe>.Validate( Recipe entity )
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