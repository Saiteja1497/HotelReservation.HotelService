namespace HotelService.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(!httpContext.Response.HasStarted)
            {
                _logger.LogInformation("Handling request: {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
            }
            if(httpContext.Response.HasStarted)
            {
                _logger.LogWarning("Response has already started, the middleware will not be executed.");
            }
            if(httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError( $"An unhandled exception has occurred while processing the request. {ex.GetType().ToString()}: {ex.Message}");

                if(ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.GetType().ToString()}: {ex.InnerException.Message}");
                }
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var response = new { Message = ex.Message, Type = ex.GetType() };
                await httpContext.Response.WriteAsJsonAsync(response);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
