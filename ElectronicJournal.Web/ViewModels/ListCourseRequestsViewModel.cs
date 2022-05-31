using ElectronicJournal.Web.Models;
using System.Collections.Generic;

namespace ElectronicJournal.Web.ViewModels
{
    public class ListCourseRequestsViewModel
    {
        public IEnumerable<CourseRequest> Requests { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
