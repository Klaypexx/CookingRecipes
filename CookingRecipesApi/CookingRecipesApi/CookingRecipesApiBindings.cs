using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.RecipesDto;
using CookingRecipesApi.Dto.UsersDto;
using FluentValidation;

namespace CookingRecipesApi;

public static class CookingRecipesApiBindings
{
    public static IServiceCollection AddCookingRecipesApi( this IServiceCollection services )
    {
        services.AddValidatorsFromAssemblyContaining<LoginDto>();
        services.AddValidatorsFromAssemblyContaining<UserDto>();
        services.AddValidatorsFromAssemblyContaining<StepDto>();
        services.AddValidatorsFromAssemblyContaining<IngredientDto>();

        return services;
    }
}
