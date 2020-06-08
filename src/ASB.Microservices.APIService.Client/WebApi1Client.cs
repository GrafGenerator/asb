using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Common.ServiceContract;
using ASB.Microservices.APIService.Client.Commands;

namespace ASB.Microservices.APIService.Client
{
    public class WebApi1Client: ServiceClient<IWebApi1Command>
    {
        public WebApi1Client(ServiceHttpTransportParameters transportParameters) : base(
            new ServiceHttpTransport(transportParameters))
        {
        }

        public async Task<SendResult<OrderDetailsResult>> GetOrderDetails(int orderId)
        {
            return await Send<OrderDetailsCommand, OrderDetailsResult>(new OrderDetailsCommand {OrderId = orderId});
        }
    }
}