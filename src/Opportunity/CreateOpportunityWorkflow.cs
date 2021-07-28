using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Opportunity
{
    public class CreateOpportunityPolicyData : ContainSagaData
    {
        public bool AccountCreatedForOpportunity { get; internal set; }
        public string AccountId { get; internal set; }
        public bool ContactCreatedForOpportunity { get; internal set; }
        public string ContactId { get; internal set; }
        public string LeadId { get; internal set; }
        public bool OpportunityCreated { get; internal set; }
        public string OpportunityId { get; internal set; }
    }

    public class CreateOpportunityWorkflow
            : Saga<CreateOpportunityPolicyData>,
        IAmStartedByMessages<CreateOpportunityFromLead>,
        IHandleMessages<OpportunityFromLeadCreated>,
        IHandleMessages<ContactFromOpportunityCreated>,
        IHandleMessages<AccountFromOpportunityCreated>
    {
        // This is where we do our business logic regarding how we create an opportunity
        private static ILog log = LogManager.GetLogger<CreateOpportunityWorkflow>();

        public async Task Handle(CreateOpportunityFromLead message, IMessageHandlerContext context)
        {
            log.Info($"CreateOpportunityWorkflow.CreateOpportunityFromLead: OpportunityId [{message.OpportunityId}] LeadId [{message.LeadId}]");
            // here we can start processing each action we need to take and at the end of the saga convert the lead
            // sent a message to the Opportunity component
            // Listen to events from other Components
            Data.LeadId = message.LeadId;
            Data.ContactId = message.ContactId;
            Data.AccountId = message.AccountId;

            await context.Send(new CreateNewOpportunityFromLead()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                LeadId = message.LeadId,
                AccountId = message.AccountId
            });
        }

        public async Task Handle(OpportunityFromLeadCreated message, IMessageHandlerContext context)
        {
            log.Info($"OpportunityFromLeadCreated: OpportunityId [{message.OpportunityId}] LeadId [{message.LeadId}]");

            Data.OpportunityCreated = true;

            await ProcessCheckFlow(context);
        }

        public async Task Handle(ContactFromOpportunityCreated message, IMessageHandlerContext context)
        {
            log.Info($"ContactFromOpportunityCreated: ContactId [{message.ContactId}] OpportunityId [{message.OpportunityId}]");

            Data.ContactCreatedForOpportunity = true;

            await context.Send(new LinkContactToOpportunity()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                LeadId = message.LeadId,
                AccountId = message.AccountId
            });

            await ProcessCheckFlow(context);
        }

        public async Task Handle(AccountFromOpportunityCreated message, IMessageHandlerContext context)
        {
            log.Info($"AccountFromOpportunityCreated:  AccountId [{message.AccountId}] OpportunityId [{message.OpportunityId}]");

            Data.AccountCreatedForOpportunity = true;

            await context.Send(new LinkAccountToOpportunity()
            {
                OpportunityId = message.OpportunityId,
                ContactId = message.ContactId,
                AccountId = message.AccountId,
                LeadId = message.LeadId
            });

            await ProcessCheckFlow(context);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CreateOpportunityPolicyData> mapper)
        {
            mapper.ConfigureMapping<CreateOpportunityFromLead>(message => message.OpportunityId)
                .ToSaga(sagaData => sagaData.OpportunityId);
            mapper.ConfigureMapping<OpportunityFromLeadCreated>(message => message.OpportunityId)
                .ToSaga(sagaData => sagaData.OpportunityId);
            mapper.ConfigureMapping<ContactFromOpportunityCreated>(message => message.OpportunityId)
                .ToSaga(sagaData => sagaData.OpportunityId);
            mapper.ConfigureMapping<AccountFromOpportunityCreated>(message => message.OpportunityId)
                .ToSaga(sagaData => sagaData.OpportunityId);
        }

        private async Task ProcessCheckFlow(IMessageHandlerContext context)
        {
            if (Data.OpportunityCreated
                && Data.ContactCreatedForOpportunity
                && Data.AccountCreatedForOpportunity)
            {
                log.Info($"ProcessCheckFlow: Completed: OpportunityId [{Data.OpportunityId}]");
                // convert the contact?
                await context.Publish(new CreateOpportunityWorkflowCompleted()
                {
                    OpportunityId = Data.OpportunityId,
                    AccountId = Data.AccountId,
                    ContactId = Data.ContactId,
                    LeadId = Data.LeadId
                });
                MarkAsComplete();
            }
        }
    }
}