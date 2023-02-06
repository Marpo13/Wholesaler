namespace Wholesaler.Backend.Domain.Exceptions
{
    public class UnpermittedActionPerformedException : Exception
    {
        public UnpermittedActionPerformedException(string message) : base(message)
        {

        }
    }
}
