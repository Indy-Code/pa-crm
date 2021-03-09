using Common.Configuration;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace EndpointHost
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "EndpointHost";

            var endpointConfiguration = new EndpointConfiguration("EndpointHost");
            endpointConfiguration.ApplyEndpointConfiguration(EndpointMappings.MessageEndpointMappings());

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}