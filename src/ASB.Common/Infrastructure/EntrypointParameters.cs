using System;
using Microsoft.Extensions.Configuration;

namespace ASB.Common.Infrastructure
{
    public class EntryPointParameters
    {
        public string ConnectionString { get; }
        public string TopicName { get; }
        public string SubscriptionName { get; }

        public EntryPointParameters(string connectionString, string topicName, string subscriptionName)
        {
            ConnectionString = connectionString ?? throw new Exception($"ServiceBus configuration invalid: {nameof(connectionString)}");;
            TopicName = topicName ?? throw new Exception($"ServiceBus configuration invalid: {nameof(topicName)}");;
            SubscriptionName = subscriptionName ?? throw new Exception($"ServiceBus configuration invalid: {nameof(subscriptionName)}");;
        }

        public static EntryPointParameters FromConfig(IConfiguration configuration, string connectionName)
        {
            var section = configuration.GetSection("Microservice");
            var connection = section.GetSection(connectionName);
            
            var connectionString = connection["connectionString"];
            var topicName = connection["topicName"];
            var subscriptionName = connection["subscriptionName"];

            return new EntryPointParameters(connectionString, topicName, subscriptionName);
        }
    }
}