using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;
using Messages.Commands;
using Messages.Events;

namespace Opportunity
{
    public class CreateOpportunityWorkflow
            : ISaga,
        InitiatedBy<CreateOpportunityFromLead>,
        Orchestrates<OpportunityFromLeadCreated>,
        Orchestrates<ContactFromOpportunityCreated>,
        Orchestrates<AccountFromOpportunityCreated>
    {
        // This is where we do our business logic regarding how we create an opportunity
        //private static ILog log = LogManager.GetLogger<CreateOpportunityWorkflow>();

        public Guid CorrelationId { get; set; }

        public Expression<Func<CreateOpportunityWorkflow, CreateOpportunityFromLead, bool>> CorrelationExpression =>
       (saga, message) => saga.CorrelationId == message.OpportunityId;

        // saga data
        public bool AccountCreatedForOpportunity { get; set; }
        public Guid AccountId { get; set; }
        public bool ContactCreatedForOpportunity { get; set; }
        public Guid ContactId { get; set; }
        public Guid LeadId { get; set; }
        public bool OpportunityCreated { get; set; }
        public Guid OpportunityId { get; set; }

        public async Task Consume(ConsumeContext<CreateOpportunityFromLead> context)
        {
            var message = context.Message;

            Console.WriteLine($"CreateOpportunityWorkflow.CreateOpportunityFromLead: OpportunityId [{context.Message.OpportunityId}] LeadId [{message.LeadId}]");
            // here we can start processing each action we need to take and at the end of the saga convert the lead
            // sent a message to the Opportunity component
            // Listen to events from other Components
            LeadId = message.LeadId;
            ContactId = message.ContactId;
            AccountId = message.AccountId;

            await context.Send(new CreateNewOpportunityFromLead()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                LeadId = message.LeadId,
                AccountId = message.AccountId
            });
        }

        public async Task Consume(ConsumeContext<OpportunityFromLeadCreated> context)
        {
            var message = context.Message;
            Console.WriteLine($"OpportunityFromLeadCreated: OpportunityId [{message.OpportunityId}] LeadId [{message.LeadId}]");

            OpportunityCreated = true;

            await ProcessCheckFlow(context);
        }

        public async Task Consume(ConsumeContext<ContactFromOpportunityCreated> context)
        {
            var message = context.Message;

            Console.WriteLine($"ContactFromOpportunityCreated: ContactId [{message.ContactId}] OpportunityId [{message.OpportunityId}]");

            ContactCreatedForOpportunity = true;

            await context.Send(new LinkContactToOpportunity()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                LeadId = message.LeadId,
                AccountId = message.AccountId
            });

            await ProcessCheckFlow(context);
        }

        public async Task Consume(ConsumeContext<AccountFromOpportunityCreated> context)
        {
            var message = context.Message;

            Console.WriteLine($"AccountFromOpportunityCreated:  AccountId [{message.AccountId}] OpportunityId [{message.OpportunityId}]");

            AccountCreatedForOpportunity = true;

            await context.Send(new LinkAccountToOpportunity()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                AccountId = message.AccountId,
                LeadId = message.LeadId
            });

            await ProcessCheckFlow(context);
        }

        private async Task ProcessCheckFlow(ConsumeContext context)
        {
            if (OpportunityCreated
                && ContactCreatedForOpportunity
                && AccountCreatedForOpportunity)
            {
                Console.WriteLine($"ProcessCheckFlow: Completed: OpportunityId [{OpportunityId}]");
                // convert the contact?
                await context.Publish(new CreateOpportunityWorkflowCompleted()
                {
                    OpportunityId = OpportunityId,
                    AccountId = AccountId,
                    ContactId = ContactId,
                    LeadId = LeadId
                });

                // complete
            }
        }
    }
}