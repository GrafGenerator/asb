using System;
using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Abstractions.Validation;
using Newtonsoft.Json;

namespace ASB.Common.Handlers
{
    public abstract class CommandHandler<T> : ICommandHandler<T> 
        where T : class, ICommand
    {
        private readonly ICommandValidator<T> _validator;

        protected CommandHandler(ICommandValidator<T> validator = null)
        {
            _validator = validator;
        }
        
        public abstract Task<CommandExecutionResult> Execute(T command);

        protected virtual async Task<T> Parse(string commandBody)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(commandBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not parse message, exception: {ex.Message}, command: {commandBody}");
                return null;
            }
        }

        public async Task<CommandExecutionResult> Execute(string commandBody)
        {
            var command = await Parse(commandBody);
            if (command == null)
            {
                return CommandExecutionResult.NotRecognized();
            }

            if (_validator != null)
            {
                var (success, errors) = _validator.Validate(command);
                if (!success)
                {
                    return CommandExecutionResult.ValidationFailure(string.Join("; ", errors));
                }
            }
            
            return await Execute(command);
        }
    }
}