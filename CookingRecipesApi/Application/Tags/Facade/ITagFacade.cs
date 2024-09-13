using Application.ResultObject;

namespace Application.Tags.Facade;

public interface ITagFacade
{
    Task<Result<List<string>>> GetRandomTagsNames();
}
