using System.Net;
using System.Text.Json;
using VehicleConditionTracker.Application.Common.Exceptions;

namespace VehicleConditionTracker.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";

            if (ex is AppException appEx)
            {
                statusCode = appEx.StatusCode;
                message = appEx.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var payload = new { message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}
