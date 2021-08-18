using System;
using System.Threading.Tasks;
using MassTransit;
using Messages.Commands;

namespace Client
{
    internal class DispatchMessagesFromApi
    {
        internal static async Task Dispatch(IBusControl endpointInstance)
        {
            var opertunityId = Guid.NewGuid();
            var leadId = Guid.NewGuid();
            var contactId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            await endpointInstance.Send(new CreateOpportunityFromLead() {
                OpportunityId = opertunityId,
                AccountId = accountId,
                LeadId = leadId,
                ContactId = contactId})
                .ConfigureAwait(false);

            await endpointInstance.Send(new CreateContactFromOpportunity() {
                OpportunityId = opertunityId,
                LeadId = leadId, 
                ContactId = contactId,
                AccountId = accountId,
                FirstName = "ContactFirstName",
                LastName = "ContactLastName"})
                .ConfigureAwait(false);

            await endpointInstance.Send(new CreateAccountFromOpportunity() {
                OpportunityId = opertunityId,
                LeadId = leadId, 
                ContactId = contactId, 
                AccountId = accountId })
                .ConfigureAwait(false);
        }
    }
}