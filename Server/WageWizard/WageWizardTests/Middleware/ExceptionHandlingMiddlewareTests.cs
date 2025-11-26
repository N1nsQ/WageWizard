using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WageWizard.Domain.Exceptions;
using WageWizard.Middleware;
using WageWizardTests.TestUtils;

namespace WageWizardTests.Middleware
{
    public class ExceptionHandlingMiddlewareTests
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private async Task<(HttpContext context, string body)> InvokeMiddleware(Exception? exception)
        {
            RequestDelegate next = ctx =>
            {
                if (exception != null)
                    throw exception;

                return Task.CompletedTask;
            };

            var middleware = new ExceptionHandlingMiddleware(next);
            var context = new DefaultHttpContext();

            context.Response.Body = new MemoryStream();
            await middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(context.Response.Body).ReadToEnd();

            return (context, body);
        }

        [Fact]
        public async Task Should_Return_401_For_UnauthorizedException()
        {
            var (context, body) = await InvokeMiddleware(new UnauthorizedException("Not allowed"));

            Assert.Equal((int)HttpStatusCode.Unauthorized, context.Response.StatusCode);

            var json = JsonSerializer.Deserialize<JsonElement>(body, _options);
            Assert.Equal("Not allowed", json.GetProperty("message").GetString());
            Assert.Equal("UnauthorizedException", json.GetProperty("type").GetString());
        }

        [Fact]
        public async Task Should_Return_404_For_KeyNotFoundException()
        {
            var (context, body) = await InvokeMiddleware(new KeyNotFoundException("Not found"));

            Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);

            var json = JsonSerializer.Deserialize<JsonElement>(body, _options);
            Assert.Equal("Not found", json.GetProperty("message").GetString());
            Assert.Equal("KeyNotFoundException", json.GetProperty("type").GetString());
        }

        [Fact]
        public async Task Returns_400_For_DomainException()
        {
            (HttpContext context, string body) =
                await InvokeMiddleware(new TestDomainException("Domain error"));

            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);

            var json = JsonSerializer.Deserialize<JsonElement>(body, _options);
            Assert.Equal("Domain error", json.GetProperty("message").GetString());
            Assert.Equal("TestDomainException", json.GetProperty("type").GetString());
        }

        [Fact]
        public async Task Should_Return_500_For_Unexpected_Exception()
        {
            var (context, body) = await InvokeMiddleware(new Exception("Boom"));

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);

            var json = JsonSerializer.Deserialize<JsonElement>(body, _options);
            Assert.Equal("An unexpected error occured", json.GetProperty("message").GetString());
            Assert.Equal("Boom", json.GetProperty("detail").GetString());
            Assert.Equal("Exception", json.GetProperty("type").GetString());
        }

        [Fact]
        public async Task Returns_500_For_RepositoryUnavailableException()
        {
            (HttpContext context, string body) =
                await InvokeMiddleware(new RepositoryUnavailableException("DB unreachable"));

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);

            var json = JsonSerializer.Deserialize<JsonElement>(body, _options);

            Assert.Equal("An unexpected error occured", json.GetProperty("message").GetString());
            Assert.Equal("RepositoryUnavailableException", json.GetProperty("type").GetString());
            Assert.Equal("DB unreachable", json.GetProperty("detail").GetString());
        }

        [Fact]
        public async Task Returns_500_For_Unexpected_Exception()
        {
            (HttpContext context, string body) =
                await InvokeMiddleware(new Exception("Unexpected crash"));

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);

            var json = JsonSerializer.Deserialize<JsonElement>(body, _options);

            Assert.Equal("An unexpected error occured", json.GetProperty("message").GetString());
            Assert.Equal("Exception", json.GetProperty("type").GetString());
            Assert.Equal("Unexpected crash", json.GetProperty("detail").GetString());
        }

        [Fact]
        public async Task Returns_500_When_FailingEmployeeRepository_Throws()
        {
            var ex = new RepositoryUnavailableException("Database down");

            (HttpContext context, string body) = await InvokeMiddleware(ex);

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);

            var json = JsonSerializer.Deserialize<JsonElement>(body, _options);

            Assert.Equal("An unexpected error occured", json.GetProperty("message").GetString());
            Assert.Equal("RepositoryUnavailableException", json.GetProperty("type").GetString());
            Assert.Equal("Database down", json.GetProperty("detail").GetString());
        }
    }
}
