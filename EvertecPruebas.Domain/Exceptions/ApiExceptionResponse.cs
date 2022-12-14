namespace EvertecPruebas.Domain.Exceptions
{
    public class ApiExceptionResponse
    {
        public string Status { get; }
        public int StatusCode { get; }
        public string Message { get; }
        public ApiExceptionResponse(string message, EnumError statusCode)
        {
            StatusCode = (int)statusCode;
            Message = message;
            Status = statusCode.ToString();
        }
    }
}
