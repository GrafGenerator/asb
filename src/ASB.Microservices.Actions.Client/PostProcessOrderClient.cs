using System.Threading.Tasks;
using ASB.Abstractions;
using ASB.Common.ServiceContract;
using ASB.Microservices.Actions.Client.Commands.PostProcessOrderCommand;

namespace ASB.Microservices.Actions.Client
{
    public class PostProcessOrderClient: ServiceClient<IPostProcessOrderCommand>
    {
        public PostProcessOrderClient(ServiceBusTransportParameters transportParameters) : base(
            new ServiceBusTransport(transportParameters))
        {
        }

        public async Task<SendResult<None>> PostProcessOrder(int orderId)
        {
            return await Send<PostProcessOrderCommand, None>(new PostProcessOrderCommand {OrderId = orderId});
        }
    }
}