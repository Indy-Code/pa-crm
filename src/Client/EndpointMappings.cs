using Messages.Commands;
using NServiceBus;
using System;

namespace Client
{
    public class EndpointMappings
    {
        internal static Action<TransportExtensions<LearningTransport>> MessageEndpointMappings()
        {
            return transport =>
            {
                var routing = transport.Routing();
                routing.RouteToEndpoint(typeof(CreateOpportunityFromLead), "EndpointHost");
                routing.RouteToEndpoint(typeof(CreateContactFromOpportunity), "EndpointHost");
                routing.RouteToEndpoint(typeof(CreateAccountFromOpportunity), "EndpointHost");
            };
        }
    }
}