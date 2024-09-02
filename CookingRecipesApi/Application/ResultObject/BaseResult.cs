using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.ResultObject;

public class BaseResult
{
    public IReadOnlyList<Error> Errors { get; protected set; }
    public bool IsSuccess { get; protected set; }
}
