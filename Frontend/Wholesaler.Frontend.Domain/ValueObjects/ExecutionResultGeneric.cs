namespace Wholesaler.Frontend.Domain.ValueObjects
{
    public class ExecutionResultGeneric
    {
        public class ExecutionResult<TResult> : ExecutionResult
        {
            public TResult Payload { get; }

            private ExecutionResult(
                bool isSuccess,
                string message,
                TResult payload) : base(isSuccess, message)
            {
                Payload = payload;
            }

            public static ExecutionResult<TResult> CreateSuccessful(TResult payload)
            {
                return new ExecutionResult<TResult>(true, null, payload);
            }

            public static ExecutionResult<TResult> CreateFailed(string message)
            {
                return new ExecutionResult<TResult>(false, message, default);
            }
        }
    }
}
