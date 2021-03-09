using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Account
{
    public class CreateAccountFromOpportunityHandler : IHandleMessages<CreateAccountFromOpportunity>
    {
        static ILog log = LogManager.GetLogger<CreateAccountFromOpportunityHandler>();

        public async Task Handle(CreateAccountFromOpportunity message, IMessageHandlerContext context)
        {
            log.Info($"CreateAccountFromOpportunityHandler: OpportunityId [{message.OpportunityId}]");

            // if the account exist we can publish a different event to specify the account already exists?

            await context.Publish(new AccountFromOpportunityCreated() {
                AccountId = message.AccountId,
                OpportunityId = message.OpportunityId,  
                ContactId = message.ContactId,
                LeadId = message.LeadId});
        }
    }
}