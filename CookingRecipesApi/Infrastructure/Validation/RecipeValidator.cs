using Application.Recipes.Entities;
using Application.ResultObject;
using Application.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Infrastructure.Validation;

public class RecipeValidator : AbstractValidator<Recipe>, Application.Validation.IValidator<Recipe>
{
    private readonly FileValidationRules _fileValidationRules;

    private const int _nameMaxWords = 50;
    private const int _descriptionMaxWords = 150;

    public RecipeValidator()
    {
        _fileValidationRules = new FileValidationRules();

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

        return new Result( result.Errors.Select( x => new Error( x.ErrorMessage ) ).ToList() );
    }
}

public class TagValidator : AbstractValidator<Tag>
{
    private const int _tagMaxWords = 20;
    public TagValidator()
    {
        RuleFor( s => s.Name )
            .MaximumLength( _tagMaxWords ).WithMessage( "Тег не должен превышать" + _tagMaxWords + "символов" );
    }
}

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

public class IngredientValidator : AbstractValidator<Ingredient>
{
    private const int _ingredientsNameMaxWords = 20;
    private const int _ingredientsProductMaxWords = 300;
    public IngredientValidator()
    {
        RuleFor( i => i.Name )
            .NotEmpty().WithMessage( "Название ингредиента обязательно" )
            .MaximumLength( _ingredientsNameMaxWords ).WithMessage( "Название ингредиента не должно превышать" + _ingredientsNameMaxWords + "символов" );

        RuleFor( i => i.Product )
            .NotEmpty().WithMessage( "Продукт обязателен" )
            .MaximumLength( _ingredientsProductMaxWords ).WithMessage( "Продукт не должен превышать" + _ingredientsProductMaxWords + "символов" );
    }
}