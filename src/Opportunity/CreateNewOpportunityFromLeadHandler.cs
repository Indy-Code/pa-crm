using System;
using System.Threading.Tasks;
using MassTransit;
using Messages.Commands;
using Messages.Events;

namespace Opportunity
{
    public class CreateNewOpportunityFromLeadHandler : IConsumer<CreateNewOpportunityFromLead>
    {
        // private static ILog log = LogManager.GetLogger<CreateNewOpportunityFromLeadHandler>();

        public async Task Consume(ConsumeContext<CreateNewOpportunityFromLead> context)
        {
            var message = context.Message;

            Console.WriteLine($"CreateNewOpportunityFromLeadHandler: OpportunityId [{message.OpportunityId}] LeadId [{message.LeadId}] ");

            await context.Publish(new OpportunityFromLeadCreated()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                LeadId = message.LeadId
            });
        }
    }
}