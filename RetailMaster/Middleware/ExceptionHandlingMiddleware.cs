using System.Net;
using System.Text.Json;
using MySqlConnector;

namespace RetailMaster.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ForeignKeyConstraintException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (DuplicateEntryException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
        } 
        catch (MySqlException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "An unexpected database error occurred: " + ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code, string message)
    {
        var result = JsonSerializer.Serialize(new { error = message, details = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        // Log the exception here
        // Example: _logger.LogError(exception, "An error occurred.");

        return context.Response.WriteAsync(result);
    }
}

public class ForeignKeyConstraintException : Exception
{
    public ForeignKeyConstraintException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class DuplicateEntryException : Exception
{
    public DuplicateEntryException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

