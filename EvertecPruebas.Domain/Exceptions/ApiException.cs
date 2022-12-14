using System.Runtime.Serialization;

namespace EvertecPruebas.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message): base(message)
        {

        }
        public ApiException(SerializationInfo info,StreamingContext context) : base(info, context)
        {

        }
    }
}
