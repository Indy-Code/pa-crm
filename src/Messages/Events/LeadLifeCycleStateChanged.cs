namespace Messages.Events
{
    public class LeadLifeCycleStateChanged
    {
        public string OpportunityId { get; set; }
        public string ContactId { get; set; }
        public string LeadId { get; set; }
        public string AccountId { get; set; }
    }
}