using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Common.ServiceContract;
using ASB.Microservices.AzureFunctions.HttpService.Client.Commands.CaptureOrder;

namespace ASB.Microservices.AzureFunctions.HttpService.Client
{
    public class AzureFunctionsHttpServiceClient: ServiceClient<IAzureFunctionsHttpServiceCommand>
    {
        public AzureFunctionsHttpServiceClient(ServiceHttpTransportParameters transportParameters) 
            : base(new ServiceHttpTransport(transportParameters))
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