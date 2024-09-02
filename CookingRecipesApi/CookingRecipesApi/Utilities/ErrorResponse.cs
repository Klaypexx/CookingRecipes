using Application.ResultObject;

namespace CookingRecipesApi.Utilities;

public class ErrorResponse
{
    public List<string> Errors { get; set; }

    public ErrorResponse( string error )
    {
        Errors = new() { error };
    }

    public ErrorResponse( IDictionary<string, string[]> errors )
    {
        Errors = new();

        foreach ( KeyValuePair<string, string[]> pair in errors )
        {
            Errors.AddRange( pair.Value );
        }
    }

    public ErrorResponse( IReadOnlyList<Error> errors )
    {
        Errors = new();

        foreach ( Error error in errors )
        {
            // Wrap the error message in an array to use AddRange
            Errors.AddRange( [ error.Message ] );
        }
    }
}

