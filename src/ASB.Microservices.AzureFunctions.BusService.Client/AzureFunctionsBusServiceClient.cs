using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Common.ServiceContract;
using ASB.Microservices.AzureFunctions.BusService.Client.Commands.CaptureOrder;

namespace ASB.Microservices.AzureFunctions.BusService.Client
{
    public class AzureFunctionsBusServiceClient: ServiceClient<IAzureFunctionsBusServiceCommand>
    {
        public AzureFunctionsBusServiceClient(ServiceBusTransportParameters transportParameters) 
            : base(new ServiceBusTransport(transportParameters))
        {
        }

        public async Task<SendResult<CaptureOrderResult>> CaptureOrder(int orderId, string userName)
        {
            return await Send<CaptureOrderCommand, CaptureOrderResult>(new CaptureOrderCommand
            {
                OrderId = orderId,
                UserName = userName
            });
        }
    }
}