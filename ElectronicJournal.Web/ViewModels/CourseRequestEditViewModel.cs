using ElectronicJournal.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicJournal.Web.ViewModels
{
    public class CourseRequestEditViewModel : CourseRequestCreateViewModel
    {
        public Guid Id { get; set; }

        public Course Course { get; set; }

        public int RequestStatusId { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public string Comment { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Updated { get; set; }

    }
}
