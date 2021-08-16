using System;
using System.Threading.Tasks;
using MassTransit;
using Messages.Commands;
using Messages.Events;

namespace Contact
{
    internal class CreateContactFromOpportunityHandlers : IConsumer<CreateContactFromOpportunity>
    {
        // private static ILog log = LogManager.GetLogger<CreateContactFromOpportunityHandlers>();

        public async Task Consume(ConsumeContext<CreateContactFromOpportunity> context)
        {
            var message = context.Message;

            Console.WriteLine($"CreateContactFromOpportunityHandlers: ContactId [{message.ContactId}] OpportunityId [{message.OpportunityId}]");

            await context.Publish(new ContactFromOpportunityCreated()
            {
                OpportunityId = message.OpportunityId,
                AccountId = message.AccountId,
                ContactId = message.ContactId,
                LeadId = message.LeadId
            });
        }
    }
}