using System.Net;

namespace SimpleRabbit.Subscriber.API.Middleware
{
    public class ExceptionHandlerMidlleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMidlleware> _logger;
        public ExceptionHandlerMidlleware(RequestDelegate next, ILogger<ExceptionHandlerMidlleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Something went wrong.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
