using Application.Common;

namespace GastiGo.API.Middleware
{

    /// <summary>
    /// permite manejar de manera centralizada las excepciones que ocurren durante el procesamiento de las solicitudes HTTP.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ocurrió un error",
                    Errors = new List<string> { ex.Message }
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
