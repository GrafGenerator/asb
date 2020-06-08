using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ASB.Abstractions;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json.Linq;

namespace ASB.Common.Infrastructure
{
    public class MessageBusMicroservice: Microservice<MessageBusMicroserviceOptions>
    {
        private SubscriptionClient _subscriptionClient;

        public MessageBusMicroservice(MicroserviceOptions<MessageBusMicroserviceOptions> options)
            :base(options)
        {
        }

        public override void Start()
        {
            _subscriptionClient = new SubscriptionClient(Options.TransportOptions.EntryPointParameters.ConnectionString,
                Options.TransportOptions.EntryPointParameters.TopicName,
                Options.TransportOptions.EntryPointParameters.SubscriptionName);
            
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            // let caller tune options if they want to
            Options.TransportOptions.ServiceBusOptionsSetupFn?.Invoke(messageHandlerOptions);
            
            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            try
            {
                var bodyText = Encoding.UTF8.GetString(message.Body);
                var result = await ProcessMessage(bodyText, token);

                switch (result.Status)
                {
                    case CommandExecutionStatus.Success:
                        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                        break;
                    case CommandExecutionStatus.Failure:
                        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                        Console.WriteLine($"Failure while processing command, message '{result.ErrorMessage}', error reason: {result.ErrorReason}");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occured in service {Options.ServiceId} while processing message, exception: {e}, message text: {Encoding.UTF8.GetString(message.Body)}");
            }
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs e)
        {
            // here exception should be logged to some aggregation service
            // for now only Console used
            Console.WriteLine($"Exception occured in service {Options.ServiceId}, exception: {e}");
            return Task.CompletedTask;
        }
    }
}