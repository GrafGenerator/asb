using System;
using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Common.Handlers;

namespace ASB.Microservices.Actions.Action3.Handlers
{
    public class PostDataCommandHandler: CommandHandler<PostDataCommand>
    {
        public override async Task<CommandExecutionResult> Execute(PostDataCommand command)
        {
            Console.WriteLine($"Received data: {command.Data}");
            return CommandExecutionResult.Ok();
        }
    }
}