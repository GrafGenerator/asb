using System.Threading;
using System.Threading.Tasks;

namespace ASB.Abstractions
{
    public interface IMessageSource
    {
        Task<CommandExecutionResult> Post(string message, CancellationToken token);
    }
}