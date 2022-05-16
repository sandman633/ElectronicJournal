using System;

namespace ElectronicJournal.Web.Models
{
    public class CourseRequest : BaseEntity
    {
        public string Reason { get; set; }
        
        public string Comment { get; set; }
        public Course Course { get; set; }

        public Guid CourseId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public User Principal { get; set; }

        public Guid PrincipalId { get; set; }
        
        public User Sender { get; set; }

        public Guid SenderId { get; set; }

        public CourseType Type { get; set; }

        public int TypeId { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public int RequestStatusId { get; set; }
    }
}