using System;

namespace Messages.Commands
{
    public class LinkAccountToOpportunity
    {
        public Guid AccountId { get; set; }
        public Guid ContactId { get; set; }
        public Guid LeadId { get; set; }
        public Guid OpportunityId { get; set; }
    }
}