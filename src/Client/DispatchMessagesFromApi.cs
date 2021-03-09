using System;
using System.Threading.Tasks;
using Messages.Commands;
using NServiceBus;

namespace Client
{
    internal class DispatchMessagesFromApi
    {
        internal static async Task Dispatch(IEndpointInstance endpointInstance)
        {
            var opertunityId = Guid.NewGuid().ToString();
            var leadId = Guid.NewGuid().ToString();
            var contactId = Guid.NewGuid().ToString();
            var accountId = Guid.NewGuid().ToString();

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