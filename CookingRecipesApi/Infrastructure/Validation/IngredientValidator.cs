using Application.Recipes.Entities;
using FluentValidation;

namespace Infrastructure.Validation;

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
