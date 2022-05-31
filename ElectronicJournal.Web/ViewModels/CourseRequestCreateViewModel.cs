using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicJournal.Web.ViewModels
{
    public class CourseRequestCreateViewModel
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public Guid CourseId { get; set; }

    }
}
