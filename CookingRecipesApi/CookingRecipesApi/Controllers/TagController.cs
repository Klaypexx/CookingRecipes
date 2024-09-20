using Application.ResultObject;
using Application.Tags.Facade;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "tags" )]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagFacade _tagFacade;

    public TagController( ITagFacade tagFacade )
    {
        _tagFacade = tagFacade;
    }

    [HttpGet]
    [Route( "random" )]
    public async Task<IActionResult> GetRandomTags()
    {
        Result<List<string>> result = await _tagFacade.GetRandomTagsNames();

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok( result.Value );
    }
}
