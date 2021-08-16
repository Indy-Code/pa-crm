using Account;
using MassTransit;
using Messages.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EndpointHost
{
    class Program
    {
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(
                cfg => { cfg.ReceiveEndpoint("EndpointHost", e =>
                {
                    e.Consumer<CreateAccountFromOpportunityHandler>();
                    // e.Consumer<>();
                });

                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });

            EndpointConvention.Map<CreateNewOpportunityFromLead>(new Uri("rabbitmq://localhost:5673/EndpointHost"));
            EndpointConvention.Map<LinkContactToOpportunity>(new Uri("rabbitmq://localhost:5673/EndpointHost"));
            EndpointConvention.Map<LinkAccountToOpportunity>(new Uri("rabbitmq://localhost:5673/EndpointHost"));
            EndpointConvention.Map<ChangeLeadLifeCycleState>(new Uri("rabbitmq://localhost:5673/EndpointHost"));

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            
            try
            {
                while (true)
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}