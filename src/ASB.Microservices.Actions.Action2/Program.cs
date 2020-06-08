using System;
using System.Threading.Tasks;
using ASB.Common.Infrastructure;
using ASB.Common.ServiceContract;
using ASB.Microservices.APIService.Client;
using ASB.Microservices.AzureFunctions.BusService.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASB.Microservices.Actions.Action2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string serviceId = "action-2";
            
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            
            var web1 = new WebApi1Client(ServiceHttpTransportParameters.FromConfig(configuration, "web1"));
            var af2 = new AzureFunctionsBusServiceClient(ServiceBusTransportParameters.FromConfig(configuration, "af2"));

            var services = new ServiceCollection();
            ConfigureServices(services);
            
            services.AddSingleton(web1);
            services.AddSingleton(af2);
            IServiceProvider provider = services.BuildServiceProvider();
            
            var microservice = new MessageBusMicroservice(new MicroserviceOptions<MessageBusMicroserviceOptions>()
            {
                ServiceId = serviceId,
                CommandsRegistry = provider.GetRequiredService<IMicroserviceCommandsRegistry>(),
                TransportOptions = new MessageBusMicroserviceOptions
                {
                    EntryPointParameters = EntryPointParameters.FromConfig(configuration, serviceId)
                }
            });
            
            microservice.Start();

            Console.WriteLine($"Service '{serviceId}' is working, press any key to exit...");
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHandlers();
            services.AddSingleton<IMicroserviceCommandsRegistry, CommandsRegistry>();
        }
    }
}