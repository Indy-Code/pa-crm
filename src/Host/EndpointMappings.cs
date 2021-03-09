using Messages.Commands;
using NServiceBus;
using System;

namespace EndpointHost
{
    public class EndpointMappings
    {
        internal static Action<TransportExtensions<LearningTransport>> MessageEndpointMappings()
        {
            return transport =>
            {
                var routing = transport.Routing();
                routing.RouteToEndpoint(typeof(CreatNewOpportunityFromLead), "EndpointHost");
                routing.RouteToEndpoint(typeof(LinkContactToOpportunity), "EndpointHost");
            };
        }
    }
}