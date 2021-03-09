using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Lead
{
    public class ChangeLeadLifeCycleStateHandler : IHandleMessages<ChangeLeadLifeCycleState>
    {
        static ILog log = LogManager.GetLogger<ChangeLeadLifeCycleStateHandler>();

        public async Task Handle(ChangeLeadLifeCycleState message, IMessageHandlerContext context)
        {
            log.Info($"ChangeLeadLifeCycleStateHandler: OpportunityId [{message.OpportunityId}]");

            await context.Publish(new LeadLifeCycleStateChanged()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                AccountId = message.AccountId,
                LeadId = message.LeadId
            });
        }
    }
}