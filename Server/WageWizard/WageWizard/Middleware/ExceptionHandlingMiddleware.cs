using System.Net;
using System.Text.Json;
using WageWizard.Domain.Exceptions;

namespace WageWizard.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerOptions _jsonOptions;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            object response;

            switch (exception)
            {
                case UnauthorizedException unauthorizedEx:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        message = unauthorizedEx.Message,
                        type = unauthorizedEx.GetType().Name
                    };
                    break;

                case KeyNotFoundException keyNotFoundEx:
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new
                    {
                        message = keyNotFoundEx.Message,
                        type = keyNotFoundEx.GetType().Name
                    };
                    break;

                case DomainException domainEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        message = domainEx.Message,
                        type = domainEx.GetType().Name
                    };
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        message = "An unexpected error occured",
                        detail = exception.Message,
                        type = exception.GetType().Name
                    };
                    break;

            }

            context.Response.StatusCode = statusCode;
            var json = JsonSerializer.Serialize(response, _jsonOptions);
            await context.Response.WriteAsync(json);
        }
    }
}
