using System.Threading.Tasks;

namespace ASB.Abstractions
{
    public interface IServiceClient<in TServiceCommand>
        where TServiceCommand: ICommand
    {
        Task<SendResult<TResult>> Send<TCommand, TResult>(TCommand command)
            where TCommand : TServiceCommand where TResult : class;
    }
}