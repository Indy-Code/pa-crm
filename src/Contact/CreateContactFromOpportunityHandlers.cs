using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Contact
{
    internal class CreateContactFromOpportunityHandlers : IHandleMessages<CreateContactFromOpportunity>
    {
        private static ILog log = LogManager.GetLogger<CreateContactFromOpportunityHandlers>();

        public async Task Handle(CreateContactFromOpportunity message, IMessageHandlerContext context)
        {
            log.Info($"CreateContactFromOpportunityHandlers: ContactId [{message.ContactId}] OpportunityId [{message.OpportunityId}]");

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