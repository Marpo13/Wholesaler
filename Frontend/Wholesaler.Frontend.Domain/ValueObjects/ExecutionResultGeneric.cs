

namespace Wholesaler.Frontend.Domain.ValueObjects
{
    public class ExecutionResultGeneric<TResult> : ExecutionResult
    {
        public TResult Payload { get; }

        private ExecutionResultGeneric(
            bool isSuccess,
            string message,
            TResult payload) : base(isSuccess, message)
        {
            Payload = payload;
        }

        public static ExecutionResultGeneric<TResult> CreateSuccessful(TResult payload)
        {
            return new ExecutionResultGeneric<TResult>(true, null, payload);
        }

        public static ExecutionResultGeneric<TResult> CreateFailed(string message)
        {
            return new ExecutionResultGeneric<TResult>(false, message, default);
        }

    }
}
