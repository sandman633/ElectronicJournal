using System.ComponentModel.DataAnnotations;

namespace ElectronicJournal.Web.ViewModels
{
    public class CourseCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
