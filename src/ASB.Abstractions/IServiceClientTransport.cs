using System.Threading.Tasks;

namespace ASB.Abstractions
{
    public interface IServiceClientTransport
    {
        Task<SendResult<TResult>> Send<TResult>(string message) where TResult : class;
    }
}