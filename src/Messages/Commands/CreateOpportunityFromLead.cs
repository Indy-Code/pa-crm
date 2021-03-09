namespace Messages.Commands
{
    public class CreateOpportunityFromLead
    {
        public string OpportunityId { get; set; }
        public string LeadId { get; set; }
        public string AccountId { get; set; }
        public string ContactId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
    }
}