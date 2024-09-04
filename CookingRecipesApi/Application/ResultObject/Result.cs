namespace Application.ResultObject;

public class Result<TValue> : BaseResult
{
    public TValue Value { get; }

    public Result( TValue value )
    {
        IsSuccess = true;
        Value = value;
    }

    public Result( Error error )
    {
        IsSuccess = false;
        Errors = [ error ];
    }

    public Result( IReadOnlyList<Error> errors )
    {
        IsSuccess = false;
        Errors = errors;
    }
}

public class Result : BaseResult
{
    public Result()
    {
        IsSuccess = true;
    }

    public Result( Error error )
    {
        IsSuccess = false;
        Errors = [ error ];
    }

    public Result( IReadOnlyList<Error> errors )
    {
        IsSuccess = false;
        Errors = errors;
    }
}