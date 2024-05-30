using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace academ_sync_back.exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                // Set status code to 400 (Bad Request) for ArgumentException
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleExceptionAsync(context, ex.Message);
            }
            catch (Exception ex)
            {
                // Set status code to 500 (Internal Server Error) for other exceptions
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(context, "An unexpected error occurred");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string message)
        {
            // Set the response content type to JSON
            context.Response.ContentType = "application/json";

            // Construct the error response message
            var errorMessage = $"{{\"message\":\"{message}\"}}";

            // Write the error response message to the response body
            return context.Response.WriteAsync(errorMessage);
        }
    }
}