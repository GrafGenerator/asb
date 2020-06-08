using System;
using Microsoft.Extensions.Configuration;

namespace ASB.Common.ServiceContract
{
    public class ServiceHttpTransportParameters
    {
        public ServiceHttpTransportParameters(string url)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public string Url { get; }

        public static ServiceHttpTransportParameters FromConfig(IConfiguration configuration, string connectionName)
        {
            var section = configuration.GetSection("Microservice");
            var connection = section.GetSection(connectionName);
            
            var url = connection["url"];
            
            return new ServiceHttpTransportParameters(url);
        }
    }
}