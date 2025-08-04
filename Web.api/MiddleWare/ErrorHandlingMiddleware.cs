namespace Web.api.MiddleWare;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorDetails) = exception switch
        {
            IdentityException exc => (exc.StatusCode, new ErrorDetails
            {
                ErrorType = "Identity Service Error",
                Message = exc.Message
            }),
            DbUpdateConcurrencyException dbUpdateConcEx => ((int)HttpStatusCode.Conflict, new ErrorDetails
            {
                ErrorType = "Data Update Conflict Error",
                Message = dbUpdateConcEx.Message,
            }),

            DbUpdateException dbUpdateEx => ((int)HttpStatusCode.BadRequest, new ErrorDetails
            {
                ErrorType = "Data Update Error",
                Message = dbUpdateEx.Message,
            }),
            NotFoundException dbUpdateEx => ((int)HttpStatusCode.NotFound, new ErrorDetails
            {
                ErrorType = "Data Update Error",
                Message = dbUpdateEx.Message,
            }),

            _ => (StatusCodes.Status500InternalServerError, new ErrorDetails
            {
                ErrorType = "Server Error",
                Message = exception.Message,
            })
        };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(errorDetails, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
}
