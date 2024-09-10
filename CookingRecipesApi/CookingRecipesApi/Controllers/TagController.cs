using Application.ResultObject;
using Application.Tags.Services;
using CookingRecipesApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CookingRecipesApi.Controllers;

[Route( "tags" )]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController( ITagService tagService )
    {
        _tagService = tagService;
    }

    [HttpGet]
    [Route( "random" )]
    public async Task<IActionResult> GetRandomTags()
    {
        Result<List<string>> result = await _tagService.GetRandomTagsNames();

        if ( !result.IsSuccess )
        {
            return BadRequest( new ErrorResponse( result.Errors ) );
        }

        return Ok( result.Value );
    }
}
