using EvertecPruebas.Domain.Exceptions;

namespace EvertecPruebas.Api.Filters
{
    public class ExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> Logger;
        private readonly RequestDelegate Delegate;
        public ExceptionFilter(ILogger<ExceptionFilter> logger, RequestDelegate _delegate)
        {
            Logger = logger;
            Delegate = _delegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Delegate(context);
            }
            catch (ApiException ex)
            {
                ApiExceptionResponse ErrorResponse = new(ex.Message, EnumError.BadRequest);
                HandleExceptionAsync(context, ex, ErrorResponse);
            }
            catch (ApiBadRequestException ex)
            {
                ApiExceptionResponse ErrorResponse = new(ex.Message, EnumError.BadRequest);
                HandleExceptionAsync(context, ex, ErrorResponse);
            }
            catch (Exception ex)
            {
                ApiExceptionResponse ErrorResponse = new(ex.Message, EnumError.InternalServerError);
                HandleExceptionAsync(context, ex, ErrorResponse);
            }

        }
        private void HandleExceptionAsync(HttpContext context, Exception ex, ApiExceptionResponse ApiResponse)
        {
            Logger.LogError(0, exception: ex, "Se presentó un error: {ex.Message}", ex.Message);
            context.Response.StatusCode = ApiResponse.StatusCode;
            context.Response.WriteAsJsonAsync(ApiResponse);

        }
    }
}
