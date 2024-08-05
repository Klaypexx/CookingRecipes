using CookingRecipesApi.Dto.RecipesDto;
using FluentValidation;

namespace CookingRecipesApi.Dto.Validators;
public class RecipeDtoValidator : AbstractValidator<RecipeDto>
{
    public RecipeDtoValidator()
    {
        RuleFor( r => r.Name )
            .NotEmpty().WithMessage( "Название рецепта обязательно" )
            .MaximumLength( 50 ).WithMessage( "Название не должно превышать 50 символов" );

        RuleFor( r => r.Description )
            .NotEmpty().WithMessage( "Описание рецепта обязательно" )
            .MaximumLength( 150 ).WithMessage( "Описание не должно превышать 150 символов" );

        RuleFor( r => r.Avatar )
            .Must( IsImage )
            .When( r => r.Avatar != null )
            .WithMessage( "Неподдерживаемый формат файла" );

        RuleFor( r => r.CookingTime )
            .NotEmpty().WithMessage( "Время приготовления обязательно" );

        RuleFor( r => r.Portion )
            .NotEmpty().WithMessage( "Количество порций обязательно" );

        RuleForEach( r => r.Tags )
           .SetValidator( new TagDtoValidator() );

        RuleForEach( r => r.Steps )
            .NotEmpty().WithMessage( "Поле шагов не должно быть пустым" )
            .SetValidator( new StepDtoValidator() );

        RuleForEach( r => r.Ingredients )
            .NotEmpty().WithMessage( "Поле ингредиентов не должно быть пустым" )
            .SetValidator( new IngredientDtoValidator() );
    }

    private bool IsImage( IFormFile file )
    {
        return file.ContentType == "image/jpeg" || file.ContentType == "image/png";
    }
}

public class TagDtoValidator : AbstractValidator<TagDto>
{
    public TagDtoValidator()
    {
        RuleFor( s => s.Name )
            .MaximumLength( 20 ).WithMessage( "Тег не должен превышать 20 символов" );
    }
}

public class StepDtoValidator : AbstractValidator<StepDto>
{
    public StepDtoValidator()
    {
        RuleFor( s => s.Description )
            .NotEmpty().WithMessage( "Описание шага обязательно" )
            .MaximumLength( 300 ).WithMessage( "Описание шага не должно превышать 300 символов" );
    }
}

public class IngredientDtoValidator : AbstractValidator<IngredientDto>
{
    public IngredientDtoValidator()
    {
        RuleFor( i => i.Name )
            .NotEmpty().WithMessage( "Название ингредиента обязательно" )
            .MaximumLength( 20 ).WithMessage( "Название ингредиента не должно превышать 20 символов" );

        RuleFor( i => i.Product )
            .NotEmpty().WithMessage( "Продукт обязателен" )
            .MaximumLength( 300 ).WithMessage( "Продукт не должен превышать 300 символов" );
    }
}