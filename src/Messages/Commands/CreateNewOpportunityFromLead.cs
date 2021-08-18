using System;

namespace Messages.Commands
{
    public class CreateNewOpportunityFromLead
    {
        public Guid OpportunityId { get; set; }
        public Guid ContactId { get; set; }
        public Guid LeadId { get; set; }
        public Guid AccountId { get; set; }
    }
}