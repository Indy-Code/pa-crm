using System;
using System.Threading.Tasks;
using MassTransit;
using Messages.Commands;
using NServiceBus;

namespace Opportunity
{
    internal class LinkContactToOpportunityHandler : IConsumer<LinkContactToOpportunity>
    {
        // private static ILog log = LogManager.GetLogger<LinkContactToOpportunityHandler>();

        public async Task Consume(ConsumeContext<LinkContactToOpportunity> context)
        {
            var message = context.Message;

            Console.WriteLine($"LinkContactToOpportunityHandler: ContactId [{message.ContactId}] OpportunityId [{message.OpportunityId}]");

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