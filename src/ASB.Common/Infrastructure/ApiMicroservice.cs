using System.Threading;
using System.Threading.Tasks;
using ASB.Abstractions;

namespace ASB.Common.Infrastructure
{
    public class ApiMicroservice: Microservice<ApiMicroserviceOptions>
    {
        public IMessageSource MessageSource { get; }
        
        public ApiMicroservice(MicroserviceOptions<ApiMicroserviceOptions> options)
            :base(options)
        {
            MessageSource = new ApiMessageSource(this);
        }

        public override void Start()
        {
        }
        
        private class ApiMessageSource: IMessageSource
        {
            private readonly ApiMicroservice _microservice;

            public ApiMessageSource(ApiMicroservice microservice)
            {
                _microservice = microservice;
            }

            public async Task<CommandExecutionResult> Post(string message, CancellationToken token)
            {
                return await _microservice.ProcessMessage(message, token);
            }
        }
    }
}