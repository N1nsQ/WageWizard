using System.Net;
using System.Text.Json;
using WageWizard.DTOs;

namespace WageWizard.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Jatketaan requestin käsittelyä
                await _next(context);
            }
            catch (Exception ex)
            {
                // Käsitellään poikkeus
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Esimerkkinä palautetaan aina 500 Internal Server Error
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponseDto
            {
                Code = "server_error",

            };

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }

    // Extension-metodi middlewaren helpompaan rekisteröintiin Startupissa/Program.cs
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
