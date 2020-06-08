using System;
using System.Threading.Tasks;
using ASB.Abstractions;
using RestSharp;

namespace ASB.Common.ServiceContract
{
    public class ServiceHttpTransport: IServiceClientTransport
    {
        private readonly RestClient _restClient;

        public ServiceHttpTransport(ServiceHttpTransportParameters transportParameters)
        {
            var url = transportParameters?.Url ?? throw new ArgumentNullException(nameof(transportParameters.Url));
            _restClient = new RestClient(url);
        }
        
        public async Task<SendResult<TResult>> Send<TResult>(string message) 
            where TResult : class
        {
            try
            {
                var request = new RestRequest("/", Method.POST)
                    {Body = new RequestBody("application/json", null!, message)};
                var response = await _restClient.ExecuteAsync<TResult>(request);

                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    return SendResult.Ok(response.Data);
                }

                return SendResult.Failure<TResult>(
                    $"Response status indicates failure (status '{response.ResponseStatus}', HTTP {response.StatusCode}, {response.StatusDescription}), message: {response.ErrorMessage}",
                    response.ErrorException);
            }
            catch (Exception ex)
            {
                return SendResult.Failure<TResult>($"HTTP transport failed: {ex.Message}", ex);
            }
        }
    }
}