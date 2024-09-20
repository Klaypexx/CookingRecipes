using Application.ResultObject;

namespace Infrastructure.Middleware;

public class ErrorResponse
{
    public List<string> Errors { get; set; }

    public ErrorResponse( string error )
    {
        Errors = new() { error };
    }

    public ErrorResponse( IReadOnlyList<Error> errors )
    {
        Errors = new();

        foreach ( Error error in errors )
        {
            Errors.AddRange( [ error.Message ] );
        }
    }
}

