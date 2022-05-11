using System;

namespace ElectronicJournal.Web.Models
{
    public class CourseRequest : BaseEntity
    {
        public string Description { get; set; }
        
        public string Reason { get; set; }

        public decimal? Price { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public User Principal { get; set; }

        public Guid PrincipalId { get; set; }
        
        public User Sender { get; set; }

        public Guid SenderId { get; set; }
    }
}