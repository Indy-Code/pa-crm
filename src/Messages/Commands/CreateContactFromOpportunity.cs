using System;

namespace Messages.Commands
{
    public class CreateContactFromOpportunity
    {
        public Guid LeadId { get; set; }
        public Guid ContactId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Guid OpportunityId { get; set; }
        public Guid AccountId { get; set; }
    }
}