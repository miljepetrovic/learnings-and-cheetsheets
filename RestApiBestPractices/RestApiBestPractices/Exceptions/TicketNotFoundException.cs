using System.Runtime.Serialization;

namespace RestApiBestPractices.Exceptions;

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException()
    {
    }

    protected TicketNotFoundException(
        SerializationInfo info, 
        StreamingContext context
    ) : base(info, context)
    {
    }

    public TicketNotFoundException(string? message) : base(message)
    {
    }

    public TicketNotFoundException(
        string? message, 
        Exception? innerException
    ) : base(message, innerException)
    {
    }
}