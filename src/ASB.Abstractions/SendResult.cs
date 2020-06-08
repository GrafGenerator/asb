using System;

namespace ASB.Abstractions
{
    public enum SendStatus
    {
        Success,
        Failure,
        ValidationFailure
    }

    public class SendResult<TResult>
        where TResult : class
    {
        public SendStatus Status { get; }
        public string ErrorMessage { get; }
        public Exception ErrorReason { get; }
        public TResult Result { get; }

        internal SendResult(SendStatus status, string errorMessage, Exception errorReason, TResult result = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
            ErrorReason = errorReason;
            Result = result;
        }
    }

    public static class SendResult
    {
        public static SendResult<TResult> Ok<TResult>(TResult result) where TResult : class =>
            new SendResult<TResult>(SendStatus.Success, null, null, result);

        public static SendResult<TResult> ValidationFailure<TResult>(string errorMessage) where TResult : class =>
            new SendResult<TResult>(SendStatus.ValidationFailure,
                $"Command not passed validation: {errorMessage}", null);

        public static SendResult<TResult> Failure<TResult>(string errorMessage, Exception errorReason = null)
            where TResult : class =>
            new SendResult<TResult>(SendStatus.Failure, errorMessage, errorReason);
    }
}