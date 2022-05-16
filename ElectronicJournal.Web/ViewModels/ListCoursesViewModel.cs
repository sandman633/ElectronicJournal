using ElectronicJournal.Web.Models;
using System;
using System.Collections.Generic;

namespace ElectronicJournal.Web.ViewModels
{
    public class ListCoursesViewModel
    {
        public IEnumerable<Course> Courses { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public Guid? TypeCourseID { get; set; }

        public IEnumerable<CourseType> CourseTypes { get; set; }

        public string NameFilter { get; set; }

    }
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
