using Common.Exceptions;

namespace Web.Middlewares
{
    public class RequestLoggingMiddleware(IConfiguration configuration, ILogger<RequestLoggingMiddleware> logger, RequestDelegate next, UserStorage userStorage)
    {
        private readonly bool _enable = configuration.GetValue<bool>("RequestLoggingMiddleware:Enable");
        private readonly ILogger<RequestLoggingMiddleware> _logger = logger;
        private readonly RequestDelegate _next = next;
        private readonly UserStorage _userStorage = userStorage;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_enable)
            {
                await _next(context);

                return;
            }

            var request = context.Request;

            if (request.Path.StartsWithSegments("/api/sign", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("RequestLoggingMiddleware: Method:{Method}, Path:{Path}{QueryString}", request.Method, request.Path, request.QueryString);

                await _next(context);

                return;
            }

            try
            {
                var userName = _userStorage.GetName();

                _logger.LogInformation("RequestLoggingMiddleware: UserName:{UserName}, Method:{Method}, Path:{Path}{QueryString}", userName, request.Method, request.Path, request.QueryString);
            }
            catch (UserNotFoundException exception)
            {
                _logger.LogError(exception, "RequestLoggingMiddleware: {ExceptionMessage}", exception.Message);

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(new { exception.Message });

                return;
            }

            await _next(context);
        }
    }
}
