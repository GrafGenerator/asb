using ASB.Abstractions;

namespace ASB.Microservices.APIService.Client.Commands
{
    public class OrderDetailsCommand: IWebApi1Command
    {
        public CommandIdentity Identity { get; } = C.OrderDetails;
        
        public int OrderId { get; set; }
    }
}