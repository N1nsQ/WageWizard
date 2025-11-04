using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WageWizard.DTOs;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _env = env ?? throw new ArgumentNullException(nameof(env));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred while processing request {Method} {Path}",
                context.Request?.Method, context.Request?.Path);


            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        if (context.Response.HasStarted)
        {
            _logger.LogWarning("Response has already started, cannot write error response for request {Path}", context.Request?.Path);

            throw exception;
        }


        var status = HttpStatusCode.InternalServerError;
        var errorCode = "server_error";

        switch (exception)
        {
            case UnauthorizedAccessException:
                status = HttpStatusCode.Unauthorized;
                errorCode = "unauthorized";
                break;
            case KeyNotFoundException:
                status = HttpStatusCode.NotFound;
                errorCode = "not_found";
                break;
        }

        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;


        var response = new ErrorResponseDto
        {
            Code = errorCode,
            Message = _env.IsDevelopment() ? exception.Message : "An unexpected error occurred."
        };

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, options);

        return context.Response.WriteAsync(json);
    }
}

// Extension
public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
