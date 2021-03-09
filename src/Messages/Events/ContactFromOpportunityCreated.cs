namespace Messages.Events
{
    public class ContactFromOpportunityCreated
    {
        public string OpportunityId { get; set; }
        public string ContactId { get; set; }
        public string AccountId { get; set; }
        public string LeadId { get; set; }
    }
}