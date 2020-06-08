using ASB.Abstractions;

namespace ASB.Microservices.AzureFunctions.BusService.Client.Commands.CaptureOrder
{
    public class CaptureOrderCommand: IAzureFunctionsBusServiceCommand
    {
        public CommandIdentity Identity { get; } = C.CatchOrder;
        
        public int OrderId { get; set; }
        public string UserName { get; set; }
    }
}