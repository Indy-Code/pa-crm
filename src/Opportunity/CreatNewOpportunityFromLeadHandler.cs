using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Opportunity
{
    public class CreatNewOpportunityFromLeadHandler : IHandleMessages<CreatNewOpportunityFromLead>
    {
        static ILog log = LogManager.GetLogger<CreatNewOpportunityFromLeadHandler>();

        public async Task Handle(CreatNewOpportunityFromLead message, IMessageHandlerContext context)
        {
            log.Info($"CreatNewOpportunityFromLeadHandler: OpportunityId [{message.OpportunityId}]");

            await context.Publish(new OpportunityFromLeadCreated() {
                OpportunityId = message.OpportunityId,  
                ContactId = message.ContactId,
                LeadId = message.LeadId});
        }
    }
}