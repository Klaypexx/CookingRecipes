using Application.Foundation;
using CookingRecipesApi.Dto.AuthDto;
using CookingRecipesApi.Dto.RecipeDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "recipes" )]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public RecipeController( IUnitOfWork unitOfWork )
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Route( "create" )]
    public async Task<IActionResult> CreateRecipe( [FromBody] RecipeDto body )
    {
        return Ok( body );
    }
}
