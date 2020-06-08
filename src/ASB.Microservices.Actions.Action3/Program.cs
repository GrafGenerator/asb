using System;
using System.Threading.Tasks;
using ASB.Common.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASB.Microservices.Actions.Action3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string serviceId = "action-3";
            
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            
            var services = new ServiceCollection();
            ConfigureServices(services);
            
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
            Console.WriteLine("Just getting all the data being posted to me and typing it here.");
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHandlers();
            services.AddSingleton<IMicroserviceCommandsRegistry, CommandsRegistry>();
        }
    }
}