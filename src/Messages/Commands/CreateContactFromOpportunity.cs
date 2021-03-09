namespace Messages.Commands
{
    public class CreateContactFromOpportunity
    {
        public string LeadId { get; set; }
        public string ContactId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string OpportunityId { get; set; }
        public string AccountId { get; set; }
    }
}