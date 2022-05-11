using System;
using System.Collections.Generic;

namespace ElectronicJournal.Web.Models
{
    public class CourseStatus : BaseEntity
    {

        public string StatusName { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
