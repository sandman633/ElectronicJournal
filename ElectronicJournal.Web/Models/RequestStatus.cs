using System.Collections.Generic;

namespace ElectronicJournal.Web.Models
{
    public class RequestStatus : BaseEntity
    {
        public string Name { get; set; }
        public int RequestStatusId { get; set; }
        public ICollection<CourseRequest> CourseRequests { get; set; }
    }
}
