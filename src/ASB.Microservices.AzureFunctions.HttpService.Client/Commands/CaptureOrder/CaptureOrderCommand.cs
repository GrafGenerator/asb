using ASB.Abstractions;

namespace ASB.Microservices.AzureFunctions.HttpService.Client.Commands.CaptureOrder
{
    public class CaptureOrderCommand: IAzureFunctionsHttpServiceCommand
    {
        public CommandIdentity Identity { get; } = C.CatchOrder;
        
        public int OrderId { get; set; }
        public string UserName { get; set; }
    }
}