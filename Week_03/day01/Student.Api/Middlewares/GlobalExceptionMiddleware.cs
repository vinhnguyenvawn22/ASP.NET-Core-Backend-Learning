namespace StudentManagement.Api.Middlewares;
using StudentManagement.Api.Responses;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            context.Response.StatusCode = 500;
            _logger.LogError(ex, "An unexpected error occurred");
            var errorResponse  = new ApiResponse<object>
            {
                Success = false,
                Message = "An unexpected error occurred",
                Data = null
            };
            await context.Response.WriteAsJsonAsync(errorResponse );
        }
        
    }
}