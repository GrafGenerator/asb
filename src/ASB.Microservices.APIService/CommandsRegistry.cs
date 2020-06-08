using System;
using System.Collections.Generic;
using System.Linq;
using ASB.Abstractions;
using ASB.Common.Infrastructure;
using ASB.Microservices.APIService.Client;
using ASB.Microservices.APIService.Client.Commands;
using ASB.Microservices.APIService.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ASB.Microservices.APIService
{
    public class CommandsRegistry: IMicroserviceCommandsRegistry
    {
        private readonly IServiceProvider _provider;

        public CommandsRegistry(IServiceProvider provider)
        {
            _provider = provider;
        }
        
        public IEnumerable<ICommandHandler> GetHandlers(string identity)
        {
            using var scope = _provider.CreateScope();
            if (HandlerTypeMap.TryGetValue(identity, out var handlerType))
            {
                return scope.ServiceProvider.GetServices(handlerType).OfType<ICommandHandler>();
            }

            return Enumerable.Empty<ICommandHandler>();
        }

        private static readonly Dictionary<CommandIdentity, Type> HandlerTypeMap = new Dictionary<CommandIdentity, Type>
        {
            {C.OrderDetails, typeof(ICommandHandler<OrderDetailsCommand>)}
        };
    }
    
    static class ServiceCollectionExtensions{
        public static IServiceCollection AddHandlers(this IServiceCollection collection)
        {
            collection.AddScoped<ICommandHandler<OrderDetailsCommand>, OrderDetailsCommandHandler>();
            
            return collection;
        }
    }
}