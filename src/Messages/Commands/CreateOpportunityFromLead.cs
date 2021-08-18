using MassTransit;
using System;

namespace Messages.Commands
{
    public class CreateOpportunityFromLead : CorrelatedBy<Guid>
    {
        public Guid OpportunityId { get; set; }
        public Guid LeadId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ContactId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }

        public Guid CorrelationId => OpportunityId;
    }
}