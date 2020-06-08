using System;
using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Common.Handlers;
using ASB.Microservices.APIService.Client.Commands;

namespace ASB.Microservices.APIService.Handlers
{
    public class OrderDetailsCommandHandler: CommandHandler<OrderDetailsCommand>
    {
        public override async Task<CommandExecutionResult> Execute(OrderDetailsCommand command)
        {
            Console.WriteLine($"Incoming request for order details for id = {command.OrderId}");
            if (command.OrderId <= 0)
            {
                return CommandExecutionResult.ValidationFailure($"Invalid order ID: {command.OrderId}");
            }

            Console.WriteLine("Getting order details...");
            var orderDetails = OrderDetailsRepo.Get(command.OrderId);
            Console.WriteLine("Ready.");
            
            return CommandExecutionResult.Ok(orderDetails);
        }
    }
}