using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.RecipesDto;
using FluentValidation;

namespace CookingRecipesApi;
public static class CookingRecipesApiBindings
{
    public static IServiceCollection AddCookingRecipesApiValidation( this IServiceCollection services )
    {
        services.AddValidatorsFromAssemblyContaining<RegisterDto>();
        services.AddValidatorsFromAssemblyContaining<LoginDto>();
        services.AddValidatorsFromAssemblyContaining<RecipeDto>();
        services.AddValidatorsFromAssemblyContaining<StepDto>();
        services.AddValidatorsFromAssemblyContaining<IngredientDto>();
        return services;
    }
}
