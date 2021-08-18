using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Messages.Commands;

namespace Client
{
    class Program
    {
        static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(
                cfg =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });

            EndpointConvention.Map<CreateOpportunityFromLead>(new Uri("rabbitmq://localhost:5673/EndpointHost"));
            EndpointConvention.Map<CreateContactFromOpportunity>(new Uri("rabbitmq://localhost:5673/EndpointHost"));
            EndpointConvention.Map<CreateAccountFromOpportunity>(new Uri("rabbitmq://localhost:5673/EndpointHost"));

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);

            while (true)
            {
                Console.WriteLine("Press 'P' to create an opportunity from lead, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the process
                        // mimic API dispatcher
                        await DispatchMessagesFromApi.Dispatch(busControl).ConfigureAwait(false);
                        break;

                    case ConsoleKey.Q:
                        await busControl.StopAsync();
                        return;

                    default:
                        Console.WriteLine("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}