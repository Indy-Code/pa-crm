using System;

namespace Lead.Data.DataModel
{
    public class LeadModel
    {
        public Guid LeadId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}