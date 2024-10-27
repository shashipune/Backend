using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HomeAssignment.API
{

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
                LogError(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void LogError(Exception exception)
        {
            // Log the exception details
            // You can adjust the logging level and message based on the exception type
            if (exception is ArgumentException)
            {
                _logger.LogWarning($"Warning: {exception.Message}");
            }
            else
            {
                _logger.LogError($"An unhandled exception has occurred: {exception.Message}", exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }


}
