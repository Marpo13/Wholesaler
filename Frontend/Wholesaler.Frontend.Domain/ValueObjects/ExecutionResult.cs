using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Frontend.Domain.ValueObjects
{
    public class ExecutionResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        protected ExecutionResult(
            bool isSuccess,
            string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static ExecutionResult CreateSuccessful()
        {
            return new ExecutionResult(true, null);
        }

        public static ExecutionResult CreateFailed(string message)
        {
            return new ExecutionResult(false, message);
        }
    }
}
