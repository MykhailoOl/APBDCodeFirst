using System.Runtime.Serialization;

namespace Task10.Exception;

public class DomainException : System.Exception
{
    public DomainException()
    {
    }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DomainException(string? message) : base(message)
    {
    }

    public DomainException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}