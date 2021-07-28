using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Opportunity
{
    public class CreateNewOpportunityFromLeadHandler : IHandleMessages<CreateNewOpportunityFromLead>
    {
        private static ILog log = LogManager.GetLogger<CreateNewOpportunityFromLeadHandler>();

        public async Task Handle(CreateNewOpportunityFromLead message, IMessageHandlerContext context)
        {
            log.Info($"CreateNewOpportunityFromLeadHandler: OpportunityId [{message.OpportunityId}] LeadId [{message.LeadId}] ");

            await context.Publish(new OpportunityFromLeadCreated()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                LeadId = message.LeadId
            });
        }
    }
}