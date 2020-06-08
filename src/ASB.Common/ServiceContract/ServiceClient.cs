using System;
using System.Threading.Tasks;
using ASB.Abstractions;
using Newtonsoft.Json;

namespace ASB.Common.ServiceContract
{
    public abstract class ServiceClient<TServiceCommand>: IServiceClient<TServiceCommand> 
        where TServiceCommand : ICommand
    {
        private readonly IServiceClientTransport _transport;

        protected ServiceClient(IServiceClientTransport transport)
        {
            _transport = transport;
        }
        
        public async Task<SendResult<TResult>> Send<TCommand, TResult>(TCommand command)
            where TCommand: TServiceCommand 
            where TResult : class
        {
            var (success, errorMessage) = ValidateCommand(command);
            if (!success)
            {
                return SendResult.ValidationFailure<TResult>(errorMessage);
            }

            var serializedCommand = JsonConvert.SerializeObject(command, new CommandIdentityJsonConverter());
            return await _transport.Send<TResult>(serializedCommand);
        }

        protected virtual (bool success, string errorMessage) ValidateCommand<TCommand>(TCommand command)
            where TCommand: TServiceCommand =>
            (true, null);

        private class CommandIdentityJsonConverter : JsonConverter<CommandIdentity>
        {
            public override void WriteJson(JsonWriter writer, CommandIdentity value, JsonSerializer serializer)
            {
                writer.WriteValue(value.Value);
            }

            public override CommandIdentity ReadJson(JsonReader reader, Type objectType, CommandIdentity existingValue, bool hasExistingValue,
                JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}