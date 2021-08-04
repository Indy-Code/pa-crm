using System;
using System.Threading.Tasks;
using Lead.Data;
using Lead.Data.DataModel;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Lead
{
    public class ChangeLeadLifeCycleStateHandler : IHandleMessages<ChangeLeadLifeCycleState>
    {
        private static ILog log = LogManager.GetLogger<ChangeLeadLifeCycleStateHandler>();
        private readonly LeadWriteRepo leadWriteRepo;

        public async Task Handle(ChangeLeadLifeCycleState message, IMessageHandlerContext context)
        {
            log.Info($"ChangeLeadLifeCycleStateHandler: LeadId [{message.LeadId}] OpportunityId [{message.OpportunityId}]");

            // sample only
            LeadModel lead = new LeadModel
            {
                LeadId = Guid.Parse(message.LeadId)
            };

            await leadWriteRepo.Add(lead).ConfigureAwait(false);

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