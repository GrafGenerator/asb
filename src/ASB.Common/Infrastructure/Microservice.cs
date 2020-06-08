using System;
using System.Threading;
using System.Threading.Tasks;
using ASB.Abstractions;
using Newtonsoft.Json.Linq;

namespace ASB.Common.Infrastructure
{
    public abstract class Microservice<TOptions>
    {
        protected readonly MicroserviceOptions<TOptions> Options;

        protected Microservice(MicroserviceOptions<TOptions> options)
        {
            if (string.IsNullOrEmpty(options.ServiceId))
            {
                throw new ArgumentNullException(nameof(options.ServiceId));
            }

            Options = options;
        }

        public abstract void Start();

        protected async Task<CommandExecutionResult> ProcessMessage(string message, CancellationToken token)
        {
            try
            {
                var messageJObject = JObject.Parse(message);
                var identity = messageJObject.GetValue("identity", StringComparison.OrdinalIgnoreCase)?.Value<string>();

                if (string.IsNullOrEmpty(identity))
                {
                    return CommandExecutionResult.Failure($"Failure while processing command, identity is missing, message: {message}");
                }

                var result = await ProcessCommand(identity, message, token);

                switch (result.Status)
                {
                    case CommandExecutionStatus.Undefined:
                        throw new MicroserviceException(
                            $"Cannot process command, identity '{identity}', body: {message}");
                    case CommandExecutionStatus.NotRecognized:
                        throw new MicroserviceException(
                            "NotRecognized is never processed on this level, check the code!!!");
                    case CommandExecutionStatus.Success:
                    case CommandExecutionStatus.Failure:
                    case CommandExecutionStatus.CriticalFailure:
                        return result;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"Service {Options.ServiceId}, cancellation requested. Cancellation exception: {e}");
                throw;
            }
            catch (MicroserviceException e)
            {
                var errorMessage = $"ATTENTION! CRITICAL EXCEPTION occured in service {Options.ServiceId} while processing message, exception: {e}, message text: {message}";
                Console.WriteLine(errorMessage);
                return CommandExecutionResult.CriticalFailure(errorMessage, e);
            }
            catch (Exception e)
            {
                var errorMessage = $"Exception occured in service {Options.ServiceId} while processing message, exception: {e}, message text: {message}";
                Console.WriteLine(errorMessage);
                throw;
            }
        }

        private async Task<CommandExecutionResult> ProcessCommand(string identity, string bodyText, CancellationToken token)
        {
            var commandHandlerVersions = Options.CommandsRegistry.GetHandlers(identity);
            foreach (var handler in commandHandlerVersions)
            {
                token.ThrowIfCancellationRequested();
                var result = await handler.Execute(bodyText);

                if (result.Status == CommandExecutionStatus.NotRecognized)
                {
                    continue;
                }

                if (result.Status == CommandExecutionStatus.ValidationFailure)
                {
                    continue;
                }

                return result;
            }

            return CommandExecutionResult.Undefined();
        }
    }
}