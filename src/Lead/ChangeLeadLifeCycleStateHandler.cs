using System;
using System.Threading.Tasks;
using Lead.Data;
using Lead.Data.DataModel;
using MassTransit;
using Messages.Commands;
using Messages.Events;

namespace Lead
{
    public class ChangeLeadLifeCycleStateHandler : IConsumer<ChangeLeadLifeCycleState>
    {
        // private static ILog log = LogManager.GetLogger<ChangeLeadLifeCycleStateHandler>();
        private readonly LeadWriteRepo leadWriteRepo;

        public async Task Consume(ConsumeContext<ChangeLeadLifeCycleState> context)
        {
            var message = context.Message;

           Console.WriteLine($"ChangeLeadLifeCycleStateHandler: LeadId [{message.LeadId}] OpportunityId [{message.OpportunityId}]");

            // sample only
            LeadModel lead = new LeadModel
            {
                LeadId = message.LeadId
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