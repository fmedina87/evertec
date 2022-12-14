using System.Runtime.Serialization;

namespace EvertecPruebas.Domain.Exceptions
{
    public class ApiBadRequestException : Exception
    {
        public ApiBadRequestException(string message): base(message)
        {

        }
        public ApiBadRequestException(SerializationInfo info,StreamingContext context) : base(info, context)
        {

        }
    }
}
