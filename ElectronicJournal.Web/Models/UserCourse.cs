using System;

namespace ElectronicJournal.Web.Models
{
    public class UserCourse : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int StatusId { get; set; }
        public CourseStatus Status { get; set; }

    }
}
