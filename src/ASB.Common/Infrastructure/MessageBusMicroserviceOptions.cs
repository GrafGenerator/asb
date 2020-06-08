using System;
using Microsoft.Azure.ServiceBus;

namespace ASB.Common.Infrastructure
{
    public class MessageBusMicroserviceOptions
    {
        public Action<MessageHandlerOptions> ServiceBusOptionsSetupFn { get; set; }
        public EntryPointParameters EntryPointParameters { get; set; }
    }
}