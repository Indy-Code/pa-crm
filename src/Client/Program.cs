using System;
using System.Threading.Tasks;
using Common.Configuration;
using NServiceBus;
using NServiceBus.Logging;

namespace Client
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "ClientUI";

            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            endpointConfiguration.ApplyEndpointConfiguration(EndpointMappings.MessageEndpointMappings());

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            await RunLoop(endpointInstance)
                .ConfigureAwait(false);

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        static ILog log = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to create an opportunity from lead, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the process
                        // mimic API dispatcher
                        await DispatchMessagesFromApi.Dispatch(endpointInstance).ConfigureAwait(false);
                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }

        }
    }
}