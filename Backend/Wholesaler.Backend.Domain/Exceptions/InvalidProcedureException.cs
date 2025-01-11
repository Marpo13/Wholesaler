namespace Wholesaler.Backend.Domain.Exceptions;

public class InvalidProcedureException : Exception
{
    public InvalidProcedureException(string message)
        : base(message)
    {
    }
}
