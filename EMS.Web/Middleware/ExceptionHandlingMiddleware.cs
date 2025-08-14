using Serilog;

namespace EMS.Web.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during request processing");
                
                // Log additional context information
                Log.Error(ex, "Request Path: {RequestPath}, Method: {RequestMethod}, User Agent: {UserAgent}",
                    context.Request.Path,
                    context.Request.Method,
                    context.Request.Headers.UserAgent.ToString());

                // Redirect to error page
                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
