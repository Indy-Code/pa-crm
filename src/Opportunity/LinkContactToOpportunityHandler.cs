using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Opportunity
{
    class LinkContactToOpportunityHandler : IHandleMessages<LinkContactToOpportunity>
    {
        static ILog log = LogManager.GetLogger<LinkContactToOpportunityHandler>();

        public async Task Handle(LinkContactToOpportunity message, IMessageHandlerContext context)
        {
            log.Info($"LinkContactToOpportunityHandler: OpportunityId [{message.OpportunityId}]");

            await context.Publish(new LeadLifeCycleStateChanged()
            {
                OpportunityId = message.OpportunityId,
                AccountId = message.AccountId,
                ContactId = message.ContactId,
                LeadId = message.LeadId
            });
        }
    }
}