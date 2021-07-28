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
                routing.RouteToEndpoint(typeof(CreateNewOpportunityFromLead), "EndpointHost");
                routing.RouteToEndpoint(typeof(LinkContactToOpportunity), "EndpointHost");
                routing.RouteToEndpoint(typeof(LinkAccountToOpportunity), "EndpointHost");
                routing.RouteToEndpoint(typeof(ChangeLeadLifeCycleState), "EndpointHost");
            };
        }
    }
}