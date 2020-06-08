using System;
using System.Text;
using System.Threading.Tasks;
using ASB.Abstractions;
using Microsoft.Azure.ServiceBus;
using RestSharp;

namespace ASB.Common.ServiceContract
{
    public class ServiceBusTransport: IServiceClientTransport
    {
        private readonly TopicClient _topicClient;

        public ServiceBusTransport(ServiceBusTransportParameters transportParameters)
        {
            var connectionString = transportParameters?.ConnectionString ?? throw new ArgumentNullException(nameof(transportParameters.ConnectionString));
            var topicName = transportParameters?.TopicName ?? throw new ArgumentNullException(nameof(transportParameters.TopicName));
            
            _topicClient = new TopicClient(connectionString, topicName);
        }
        
        public async Task<SendResult<TResult>> Send<TResult>(string message) 
            where TResult : class
        {
            try
            {
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await _topicClient.SendAsync(new Message(messageBytes));

                return SendResult.Ok<TResult>(null);
            }
            catch (Exception ex)
            {
                return SendResult.Failure<TResult>($"MessageBus transport failed: {ex.Message}", ex);
            }
        }
    }
}