using System;
using System.Collections.Generic;

namespace ElectronicJournal.Web.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public CourseType Type { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public Guid TypeId { get; set; }

        public User Teacher { get; set; } 

        public Guid? TeacherId { get; set; } 

        public ICollection<UserCourse> UserCourses { get; set; }
    }
}