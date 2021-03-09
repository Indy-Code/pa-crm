namespace Messages.Commands
{
    public class CreateAccountFromOpportunity
    {
        public string OpportunityId;
        public string LeadId { get; set; }
        public string ContactId { get; set; }
        public string AccountId { get; set; }
    }
}