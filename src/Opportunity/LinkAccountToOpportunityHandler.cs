using System;
using System.Threading.Tasks;
using MassTransit;
using Messages.Commands;
using NServiceBus;

namespace Opportunity
{
    internal class LinkAccountToOpportunityHandler : IConsumer<LinkAccountToOpportunity>
    {
        // private static ILog log = LogManager.GetLogger<LinkContactToOpportunityHandler>();

        public async Task Consume(ConsumeContext<LinkAccountToOpportunity> context)
        {
            var message = context.Message;

            Console.WriteLine($"LinkAccountToOpportunityHandler: AccountId [{message.AccountId}] OpportunityId [{message.OpportunityId} ]");

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