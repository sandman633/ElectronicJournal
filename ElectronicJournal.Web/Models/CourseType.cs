using System;
using System.Collections.Generic;

namespace ElectronicJournal.Web.Models
{
    public class CourseType : BaseEntity
    {
        public string Name { get; set; }
        public int TypeId { get;  set; }
        public ICollection<Course> Courses { get;  set; }
    }
}