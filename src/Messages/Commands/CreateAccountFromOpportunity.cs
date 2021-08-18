using System;

namespace Messages.Commands
{
    public class CreateAccountFromOpportunity
    {
        public Guid OpportunityId;
        public Guid LeadId { get; set; }
        public Guid ContactId { get; set; }
        public Guid AccountId { get; set; }
    }
}