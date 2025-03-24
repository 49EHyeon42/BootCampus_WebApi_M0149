using Common.Exceptions;

namespace Web.Middlewares
{
    public class RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger, RequestDelegate next, UserStorage userStorage)
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger = logger;
        private readonly RequestDelegate _next = next;
        private readonly UserStorage _userStorage = userStorage;

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            if (request.Path.StartsWithSegments("/api/sign", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("{DateTime} RequestLoggingMiddleware: Method:{Method}, Path:{Path}{QueryString}", DateTime.UtcNow, request.Method, request.Path, request.QueryString);

                await _next(context);

                return;
            }

            try
            {
                var userName = _userStorage.GetName();

                _logger.LogInformation("{DateTime} RequestLoggingMiddleware: UserName:{UserName}, Method:{Method}, Path:{Path}{QueryString}", DateTime.UtcNow, userName, request.Method, request.Path, request.QueryString);
            }
            catch (UserNotFoundException exception)
            {
                _logger.LogError(exception, "{DateTime} RequestLoggingMiddleware: {ExceptionMessage}", DateTime.UtcNow, exception.Message);

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(new { exception.Message });

                return;
            }

            await _next(context);
        }
    }
}
