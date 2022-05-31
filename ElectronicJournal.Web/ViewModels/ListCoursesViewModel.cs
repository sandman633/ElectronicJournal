using ElectronicJournal.Web.Models;
using System;
using System.Collections.Generic;

namespace ElectronicJournal.Web.ViewModels
{
    public class ListCoursesViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public Guid UserId { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string Status { get; set; }
        public string NameFilter { get; set; }
        public bool ShowAll { get;  set; }
    }
}
