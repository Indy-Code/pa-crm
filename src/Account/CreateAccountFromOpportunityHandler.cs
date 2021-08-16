using System;
using System.Threading.Tasks;
using MassTransit;
using Messages.Commands;
using Messages.Events;

namespace Account
{
    public class CreateAccountFromOpportunityHandler : IConsumer<CreateAccountFromOpportunity>
    {
        // private static ILog log = LogManager.GetLogger<CreateAccountFromOpportunityHandler>();

        public async Task Consume(ConsumeContext<CreateAccountFromOpportunity> context)
        {
            Console.WriteLine($"CreateAccountFromOpportunityHandler: AccountId [{context.Message.AccountId}] OpportunityId [{context.Message.OpportunityId}]");

            // if the account exist we can publish a different event to specify the account already exists?

            await context.Publish(new AccountFromOpportunityCreated()
            {
                AccountId = context.Message.AccountId,
                OpportunityId = context.Message.OpportunityId,
                ContactId = context.Message.ContactId,
                LeadId = context.Message.LeadId
            });
        }
    }
}