using System.Threading.Tasks;

namespace ASB.Abstractions
{
    public interface ICommandHandler
    {
        Task<CommandExecutionResult> Execute(string commandBody);
    }

    public interface ICommandHandler<T> : ICommandHandler
        where T: ICommand
    {
        Task<CommandExecutionResult> Execute(T command);
    }
}