using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware( RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync( HttpContext context )
    {
        try
        {
            await _next( context );
        }
        catch ( Exception ex )
        {
            await HandleExceptionAsync( context, ex );
        }
    }

    private async Task HandleExceptionAsync( HttpContext context, Exception exception )
    {
        _logger.LogError( exception, "An unexpected error occurred." );

        ErrorResponse response = new( "Internal server error. Please retry later." );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync( response );
    }
}