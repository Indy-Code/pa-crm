using System;

namespace Messages.Events
{
    public class LeadLifeCycleStateChanged
    {
        public Guid OpportunityId { get; set; }
        public Guid ContactId { get; set; }
        public Guid LeadId { get; set; }
        public Guid AccountId { get; set; }
    }
}