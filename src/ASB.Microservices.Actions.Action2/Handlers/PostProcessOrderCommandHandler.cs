using System;
using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Common.Handlers;
using ASB.Microservices.Actions.Client.Commands.PostProcessOrderCommand;
using ASB.Microservices.APIService.Client;
using ASB.Microservices.AzureFunctions.BusService.Client;

namespace ASB.Microservices.Actions.Action2.Handlers
{
    public class PostProcessOrderCommandHandler: CommandHandler<PostProcessOrderCommand>
    {
        private readonly WebApi1Client _web1;
        private readonly AzureFunctionsBusServiceClient _azureFunctionsBusServiceClient;

        public PostProcessOrderCommandHandler(WebApi1Client web1, AzureFunctionsBusServiceClient azureFunctionsBusServiceClient)
        {
            _web1 = web1;
            _azureFunctionsBusServiceClient = azureFunctionsBusServiceClient;
        }
        
        public override async Task<CommandExecutionResult> Execute(PostProcessOrderCommand command)
        {
            if (command.OrderId <= 0)
            {
                return CommandExecutionResult.ValidationFailure($"Invalid order ID: {command.OrderId}");
            }

            var orderId = command.OrderId;
            Console.WriteLine($"Post-process order id = {orderId}");
            
            Console.WriteLine("Getting order details...");
            var orderDetailsResult = await _web1.GetOrderDetails(orderId);
            if (orderDetailsResult.Status != SendStatus.Success || orderDetailsResult.Result == null)
            {
                Console.WriteLine("Getting order details failed.");
                return CommandExecutionResult.Failure(orderDetailsResult.ErrorMessage, orderDetailsResult.ErrorReason);
            }

            var orderDetails = orderDetailsResult.Result;
            
            Console.WriteLine($"Retrieved order details: user is (id {orderDetails.UserId}, name {orderDetails.UserName}), order amount is {orderDetails.Amount}, created on {orderDetails.DateCreated}");
            
            Console.WriteLine($"Pass order details to Azure Functions Service Bus microservice...");

            var captureResult = await _azureFunctionsBusServiceClient.CaptureOrder(orderDetails.OrderId, orderDetails.UserName);

            if (captureResult.Status != SendStatus.Success)
            {
                Console.WriteLine($"Calling Azure Functions microservice failed: {captureResult.ErrorMessage}");
            }
            else
            {
                Console.WriteLine($"Message sent successfully'");
            }
            
            return CommandExecutionResult.Ok();
        }
    }
}