using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Opportunity
{
    internal class LinkAccountToOpportunityHandler : IHandleMessages<LinkAccountToOpportunity>
    {
        private static ILog log = LogManager.GetLogger<LinkContactToOpportunityHandler>();

        public async Task Handle(LinkAccountToOpportunity message, IMessageHandlerContext context)
        {
            log.Info($"LinkAccountToOpportunityHandler: AccountId [{message.AccountId}] OpportunityId [{message.OpportunityId} ]");

            await context.Send(new ChangeLeadLifeCycleState()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                AccountId = message.AccountId,
                LeadId = message.LeadId
            });
        }
    }
}