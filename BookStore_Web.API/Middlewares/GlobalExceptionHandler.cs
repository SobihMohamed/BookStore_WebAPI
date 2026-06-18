using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using static Domain.Exception_Handle.Exceptions;

namespace BookStore_Web.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IWebHostEnvironment _env;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public async ValueTask<bool> TryHandleAsync(
             HttpContext httpContext,
             Exception exception,
             CancellationToken cancellationToken)
        {
            //
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            if (httpContext.Response.HasStarted)
            {
                _logger.LogWarning("Response has already started, skipping global exception handler.");
                return false;
            }

            // used in development environment to provide detailed error information
            string detailedMessage = exception.InnerException != null
                ? $"{exception.Message} \n Inner Details: {exception.GetBaseException().Message}"
                : exception.Message;
            // if the environment is development, include the stack trace in the response, otherwise provide a generic error message
            string serverErrorMessage = _env.IsDevelopment()
                ? $"{detailedMessage} \n\nStackTrace:\n{exception.StackTrace}"
                : "An unexpected error occurred. Please try again later.";

            // check the type of exception and set the appropriate status code and message
            var (statusCode, message, errors) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, exception.Message, (List<string>?)null),
                UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message, null),
                BadRequestException br => (StatusCodes.Status400BadRequest, br.Message, br.Errors?.ToList()),
                _ => (StatusCodes.Status500InternalServerError, serverErrorMessage, null)
            };

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            // 4. استخدام الـ ApiResponse الـ Wrapper بتاعك
            //var response = new ApiResponse<string>(message, statusCode, errors)
            //{
            //    IsSuccess = false
            //};

            // 5. إرسال الـ Response بشكل تلقائي كـ JSON بدون تعقيد السيرياليزر اليدوي
            //await httpContext.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //}, cancellationToken);

            // نرجع true عشان نعرف الـ Pipeline إن الإيرور اتمسك واتعالج بنجاح
            return true;
        }
    }
}
