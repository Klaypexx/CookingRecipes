using Application.ResultObject;
using Application.Tags.Services;

namespace Application.Tags.Facade;

public class TagFacade : ITagFacade
{
    private readonly ITagService _tagService;

    public TagFacade( ITagService tagService )
    {
        _tagService = tagService;
    }

    public async Task<Result<List<string>>> GetRandomTagsNames()
    {
        try
        {
            List<string> randomTagsNames = await _tagService.GetRandomTagsNames();

            return new Result<List<string>>( randomTagsNames );
        }
        catch ( Exception e )
        {
            return new Result<List<string>>( new Error( e.Message ) );
        }
    }
}
