using System;

namespace ASB.Abstractions
{
    public enum CommandExecutionStatus
    {
        Undefined,
        NotRecognized,
        ValidationFailure,
        Success,
        Failure,
        CriticalFailure,
    }
    public class CommandExecutionResult
    {
        public CommandExecutionStatus Status { get; }
        public string ErrorMessage { get; }
        public Exception ErrorReason { get; }
        public object Object { get; }

        private CommandExecutionResult(CommandExecutionStatus status, string errorMessage, Exception errorReason, object o = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
            ErrorReason = errorReason;
            Object = o;
        }
        
        public static CommandExecutionResult Undefined() => new CommandExecutionResult(CommandExecutionStatus.Undefined, "Could not execute command", null);
        public static CommandExecutionResult Ok(object result = null) => new CommandExecutionResult(CommandExecutionStatus.Success, null, null, result);
        public static CommandExecutionResult NotRecognized() => new CommandExecutionResult(CommandExecutionStatus.NotRecognized, "Command not recognized", null);
        public static CommandExecutionResult ValidationFailure(string errorMessage) => new CommandExecutionResult(CommandExecutionStatus.ValidationFailure, $"Command not passed validation: {errorMessage}", null);
        public static CommandExecutionResult Failure(string errorMessage, Exception errorReason = null) => new CommandExecutionResult(CommandExecutionStatus.Failure, errorMessage, errorReason);
        public static CommandExecutionResult CriticalFailure(string errorMessage, Exception errorReason = null) => new CommandExecutionResult(CommandExecutionStatus.CriticalFailure, errorMessage, errorReason);
    }
}