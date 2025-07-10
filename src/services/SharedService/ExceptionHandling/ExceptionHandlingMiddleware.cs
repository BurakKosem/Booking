using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SharedService.ExceptionHandling;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred. TraceId: {TraceId}", context.TraceIdentifier);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var result = exception switch
        {
            ArgumentException argEx => Result.Result.Failure(argEx.Message, HttpStatusCode.BadRequest),
            UnauthorizedAccessException => Result.Result.Failure("Access denied", HttpStatusCode.Unauthorized),
            KeyNotFoundException => Result.Result.Failure("Resource not found", HttpStatusCode.NotFound),
            InvalidOperationException invalidEx => Result.Result.Failure(invalidEx.Message, HttpStatusCode.Conflict),
            TimeoutException => Result.Result.Failure("Request timeout", HttpStatusCode.RequestTimeout),
            NotImplementedException => Result.Result.Failure("Feature not implemented", HttpStatusCode.NotImplemented),
            _ => Result.Result.Failure("An internal server error occurred", HttpStatusCode.InternalServerError)
        };

        context.Response.StatusCode = (int)result.StatusCode;

        var response = new
        {
            success = false,
            error = result.ErrorMessage,
            traceId = context.TraceIdentifier,
            timestamp = DateTime.UtcNow
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
