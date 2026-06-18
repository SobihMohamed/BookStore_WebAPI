using System.Text.Json;

namespace BookStore_Web.API.Middlewares
{
    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddCustomExceptionHandling(this IServiceCollection services)
        {
            //add the custom exception handler to the service collection
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails(); // used for generating standardized error responses
            return services;
        }

        public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
        {
            // catch the 404 Not Found errors and return a JSON response instead of the default HTML response
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    //var response = new ApiResponse<string>("The requested resource was not found.", StatusCodes.Status404NotFound)
                    //{
                    //    IsSuccess = false
                    //};

                    //await context.HttpContext.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
                    //{
                    //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    //});
                }
            });

            // add the custom exception handler middleware to the application pipeline
            app.UseExceptionHandler();

            return app;
        }
    }
}