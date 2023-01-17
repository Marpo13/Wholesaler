namespace Wholesaler.Backend.Domain.Exceptions
{
    public class InvalidDataProvidedException : Exception
    {               
        public InvalidDataProvidedException(string message) : base(message)
        {

        }
    }
}
