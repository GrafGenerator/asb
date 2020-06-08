using System.Collections.Generic;
using ASB.Abstractions;

namespace ASB.Common.Infrastructure
{
    public interface IMicroserviceCommandsRegistry
    {
        /// <summary>
        /// Map of handlers for commands.
        /// Each command is represented by it's identity (string), for which
        /// 1 or more handlers may be assigned.
        /// Handlers are called in the order of their declaration in list.
        /// If handler did not recognize command, it must return appropriate status (NotRecognized).
        /// If status is different from NotRecognized, the response is analyzed and decision taken depending on the
        /// status. Iterating through handlers stops in this case.
        /// </summary>
        IEnumerable<ICommandHandler> GetHandlers(string identity);
    }
}