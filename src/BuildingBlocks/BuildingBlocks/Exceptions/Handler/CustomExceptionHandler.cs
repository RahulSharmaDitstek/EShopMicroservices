namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;
        logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", exceptionMessage, DateTime.UtcNow);

        (string Detail, string Title, int StatusCode) = exception switch
        {
            InternalServerException =>
            (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),
            ValidationException =>
            (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            BadRequestException =>
            (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            NotFoundException =>
            (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),

            _ =>
           (
           exception.Message,
           exception.GetType().Name,
           context.Response.StatusCode = StatusCodes.Status400BadRequest
           ),
        };
        var problemDetails = new ProblemDetails
        {
            Title = Title,
            Detail = Detail,
            Status = StatusCode,
            Instance = context.Request.Path
        };
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        if (exception is ValidationException validationException)
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
