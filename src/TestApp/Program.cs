using System;
using System.Threading.Tasks;
using ASB.Common.ServiceContract;
using ASB.Microservices.Actions.Client;
using Microsoft.Extensions.Configuration;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            var postProcessClient = new PostProcessOrderClient(ServiceBusTransportParameters.FromConfig(configuration, "post-process-group"));

            WritePrompt();

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine()?.ToLower();

                if (line == "q")
                {
                    break;
                }
                
                switch (line)
                {
                    case "h": 
                        WritePrompt();
                        break;
                    case { } s when int.TryParse(s, out var id):
                        Console.WriteLine($"Sending command for order id = {id}");
                        await postProcessClient.PostProcessOrder(id);
                        Console.WriteLine($"Command sent, time = {DateTime.Now}");
                        break;
                }
            }
            
            Console.WriteLine("Terminating...");
        }

        static void WritePrompt()
        {
            Console.WriteLine("====== Test Application ======");
            Console.WriteLine("This is entry point into sample microservices architecture.");
            Console.WriteLine("Part of application being simulated is order post-processing.");
            Console.WriteLine("Enter order number (any non negative integer) and press Enter. App will send appropriate message to service bus, imitating the fact that order is payed successfully and ready for post-processing.");
            Console.WriteLine("Enter 'q' (w/o quotes) to exit.");
            Console.WriteLine("Enter 'h' (w/o quotes) to see this message again.");
        }
    }
}