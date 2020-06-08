using System;
using Microsoft.Extensions.Configuration;

namespace ASB.Common.ServiceContract
{
    public class ServiceBusTransportParameters
    {
        public ServiceBusTransportParameters(string connectionString, string topicName)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            TopicName = topicName ?? throw new ArgumentNullException(nameof(topicName));
        }

        public string ConnectionString { get; }
        public string TopicName { get; }

        public static ServiceBusTransportParameters FromConfig(IConfiguration configuration, string connectionName)
        {
            var section = configuration.GetSection("Microservice");
            var connection = section.GetSection(connectionName);
            
            var connectionString = connection["connectionString"];
            var topicName = connection["topicName"];
            
            return new ServiceBusTransportParameters(connectionString, topicName);
        }
    }
}