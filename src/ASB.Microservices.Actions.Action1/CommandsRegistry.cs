using System;
using System.Collections.Generic;
using System.Linq;
using ASB.Abstractions;
using ASB.Common.Infrastructure;
using ASB.Microservices.Actions.Action1.Handlers;
using ASB.Microservices.Actions.Client;
using ASB.Microservices.Actions.Client.Commands.PostProcessOrderCommand;
using Microsoft.Extensions.DependencyInjection;

namespace ASB.Microservices.Actions.Action1
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
            {C.PostProcessOrder, typeof(ICommandHandler<PostProcessOrderCommand>)}
        };
    }
    
    static class ServiceCollectionExtensions{
        public static IServiceCollection AddHandlers(this IServiceCollection collection)
        {
            collection.AddScoped<ICommandHandler<PostProcessOrderCommand>, PostProcessOrderCommandHandler>();
            
            return collection;
        }
    }
}