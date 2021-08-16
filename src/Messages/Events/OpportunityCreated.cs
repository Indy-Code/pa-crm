using MassTransit;
using System;

namespace Messages.Events
{
    public class OpportunityFromLeadCreated : CorrelatedBy<Guid>
    {
        public Guid OpportunityId { get; set; }
        public Guid ContactId { get; set; }
        public Guid LeadId { get; set; }
        public Guid AccountId { get; set; }

        public Guid CorrelationId =>  OpportunityId;
    }
}